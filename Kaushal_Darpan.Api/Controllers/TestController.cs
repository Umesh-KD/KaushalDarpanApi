using AspNetCore.Reporting;
using Kaushal_Darpan.Core.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Kaushal_Darpan.Models.DateConfiguration;
using Newtonsoft.Json;
using Kaushal_Darpan.Core.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Kaushal_Darpan.Api.Code.Helper;
using Kaushal_Darpan.Models.Test;
using Kaushal_Darpan.Models.SMSConfigurationSetting;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        public override string PageName => "TestController";
        public override string ActionName { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        private readonly SMSConfigurationSettingModel _sMSConfigurationSetting;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _sMSConfigurationSetting = _unitOfWork.SMSMailRepository.GetSMSConfigurationSetting().Result;
        }

        [HttpPost("Test")]
        public string Test([FromForm] Test model)
        {
            var str = $"welcome ({model.Id}) {model.Name}";
            return str;
        }

        [HttpGet("Test2")]
        public void Test2()
        {
            Response.Redirect("https://www.google.com/", false);
        }
        [HttpGet("Hello")]
        public string Hello()
        {
            return "hi KD Api";
        }

        [HttpPost("ff")]
        public void ff(Test test)
        {

        }
        [HttpPost("AdmissionDateTest")]
        public DateConfigurationModel AdmissionDateTest([FromForm] DateConfigurationModel dateConfiguration)
        {
            return dateConfiguration;
        }

        [HttpPost("RdlcReport")]
        public void RdlcReport()
        {
            string bterLogo = new Uri($"{ConfigurationHelper.StaticFileRootPath}{Constants.LogFolder}/bter_logo.jpg").AbsoluteUri;
            //report
            var fileName = $"test.pdf";
            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/test.rdlc";
            //
            var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
            string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/sign.png";
            var stuimg = System.IO.File.ReadAllBytes(stuimgFilepath);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("qr", Convert.ToBase64String(qrcode));
            //
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            LocalReport localReport = new LocalReport(rdlcpath);
            var data = new DataTable();
            data.Columns.Add("qrcode", typeof(byte[]));
            data.Columns.Add("stuimg", typeof(byte[]));
            var row = data.NewRow();
            row["qrcode"] = qrcode;
            row["stuimg"] = stuimg;
            data.Rows.Add(row);
            localReport.AddDataSource("test", data);
            var reportResult = localReport.Execute(RenderType.Pdf);
            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
            //end report
        }


        [HttpPost("Dummy_SaveAndMoveStudentImages")]
        public async Task<string> Dummy_SaveAndMoveStudentImages()
        {
            try
            {
                //--------paths--------
                //old (source):
                //Sem. Eng. = \StaticFiles\old-bter-student-images\SemEng\documents\1\
                //Sem. NonEng. = \StaticFiles\old-bter-student-images\SemNonEng\documents\1\
                //Year Eng. = \StaticFiles\old-bter-student-images\YearEng\documents\1\
                //Year NonEng. = \StaticFiles\old-bter-student-images\YearNonEng\documents\1\

                //new (destination):
                //All = \StaticFiles\Students\BTER\2025\1\20\


                //log
                CommonFuncationHelper.WriteTextLog("Dummy_SaveAndMoveStudentImages start:");

                //data
                var action = "_Dummy_GetStudentDataForStudentsIds";
                var ds = await _unitOfWork.CommonFunctionRepository.Dummy_GetTestUspDataByAction(action);
                DataTable dataTable = ds.Tables[0];
                //log
                CommonFuncationHelper.WriteTextLog($"Dummy_GetTestUspDataByAction getdata count = {dataTable.Rows.Count}");

                //source path
                string sourceRootPath = System.IO.Path.Combine(ConfigurationHelper.StaticFileRootPath, "old-bter-student-images");
                string destinationRootPath = System.IO.Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "BTER");

                if (!Directory.Exists(sourceRootPath))
                {
                    return "sourceRootPath Directory does not exist!";
                }
                if (!Directory.Exists(destinationRootPath))
                {
                    Directory.CreateDirectory(destinationRootPath);
                }


                //loop files each student
                //log
                CommonFuncationHelper.WriteTextLog("table loop start:");
                CommonFuncationHelper.WriteTextLog("all file copy start:");
                int i = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    ++i;

                    //make full path of each students old id
                    var sourceFolderPathEach = System.IO.Path.Combine(sourceRootPath, row["FolderType"].ToString(), "documents", row["StudentID_Old"].ToString());

                    //log
                    CommonFuncationHelper.WriteTextLog($"source folder ({sourceFolderPathEach}) loop count = {i}");

                    //check path
                    if (!Directory.Exists(sourceFolderPathEach))
                    {
                        //log
                        CommonFuncationHelper.WriteTextLog($"source folder path not found = {sourceFolderPathEach}");
                        continue;
                    }
                    //get files
                    int j = 0;
                    string[] files = Directory.GetFiles(sourceFolderPathEach);
                    foreach (var file in files)
                    {
                        ++j;

                        //source file path
                        string oldPath = file;

                        //log
                        CommonFuncationHelper.WriteTextLog($"source file ({oldPath}) loop count = {j}");
                        try
                        {
                            //check source file
                            if (!System.IO.File.Exists(oldPath))
                            {
                                //log
                                CommonFuncationHelper.WriteTextLog($"source file path not found = {oldPath}");
                                continue;
                            }

                            //make full path of each students new id
                            var destinationFolderPathEach = System.IO.Path.Combine(destinationRootPath, row["FolderYear"].ToString(), row["CourseType"].ToString(), row["StudentID"].ToString());

                            //destination path
                            if (!Directory.Exists(destinationFolderPathEach))
                            {
                                Directory.CreateDirectory(destinationFolderPathEach);
                            }
                            var fileName = System.IO.Path.GetFileName(oldPath);
                            string newPath = System.IO.Path.Combine(destinationFolderPathEach, fileName);

                            //----- copy file in new folder structure -----
                            // copy the file
                            // Open the source file with shared read access
                            using (var sourceStream = new FileStream(oldPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
                            using (var destinationStream = new FileStream(newPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                            {
                                await sourceStream.CopyToAsync(destinationStream);
                            }

                            //set document master id for student master (1=photo,2=sign)
                            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                            var docMasterId = 0;
                            if (fileNameWithoutExt == "photo" || fileNameWithoutExt == "photograph")
                            {
                                docMasterId = 1;
                            }
                            else if (fileNameWithoutExt == "signature" || fileNameWithoutExt == "sign")
                            {
                                docMasterId = 2;
                            }

                            // set in table for document table save
                            row["DocumentMasterID"] = docMasterId;
                            row["FileName"] = fileName;
                            row["Dis_FileName"] = fileName;
                        }
                        catch (Exception ex)
                        {
                            //log
                            CommonFuncationHelper.WriteTextLog($"Failed to copy file: {oldPath}. Error: {ex.Message}");
                        }
                    }
                }

                //log
                CommonFuncationHelper.WriteTextLog("all file copy end:");
                CommonFuncationHelper.WriteTextLog("table loop end:");

                CommonFuncationHelper.WriteTextLog("DB Insert start:");
                //convert in json
                string json = JsonConvert.SerializeObject(dataTable);

                //log
                CommonFuncationHelper.WriteTextLog($"DB Insert Start with json : {json}");

                //db
                var r = await _unitOfWork.CommonFunctionRepository.Dummy_SaveAndMoveStudentImages(json);
                _unitOfWork.SaveChanges();

                //log
                CommonFuncationHelper.WriteTextLog("DB Insert end:");

                return "Success";
            }
            catch (Exception ex)
            {
                //log
                CommonFuncationHelper.WriteTextLog($"Error: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

        [HttpPost]
        public IActionResult SaveBase64File([FromBody] string base64String)
        {
            try
            {
                string originalFileName = "test.jpg";

                // 1. Decode Base64
                byte[] fileBytes = Convert.FromBase64String(base64String);

                // 2. Define upload folder
                //string uploadsFolder = Server.MapPath("~/Uploads");

                // 2. Define upload folder (use wwwroot/Uploads)
                string uploadsFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // 3. Create unique filename
                string fileName = Path.GetFileName(originalFileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    string extension = Path.GetExtension(fileName);
                    fileName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}{extension}";
                    filePath = Path.Combine(uploadsFolder, fileName);
                }

                // 4. Save file
                System.IO.File.WriteAllBytes(filePath, fileBytes);

                var r = new { success = true, fileName = fileName };
                return Ok(r);
            }
            catch (Exception ex)
            {
                var r = new { success = false, message = ex.Message };
                return BadRequest(r);
            }
        }

        [HttpPost("testwordfile")]
        public string testwordfile()
        {
            try
            {
                // Step 1: Create individual documents
                // Make sure this path exists
                string folderPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, "WordFiles");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                List<string> docPaths = new List<string>();
                for (int i = 1; i <= 3; i++)
                {
                    var guid = Guid.NewGuid();
                    var path = Path.Combine(folderPath, $"SampleWord_{guid.ToString()}.docx");
                    CreateWordDocument(path, $"This is document-{i} for {guid.ToString()}");//create
                    docPaths.Add(path);
                }

                // Step 2: Merge documents
                // Make sure this path exists
                folderPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, "WordFiles", "MergedFiles");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var guid1 = Guid.NewGuid();
                string outputPath = Path.Combine(folderPath, $"Merged_{guid1.ToString()}.docx");
                WordHelper.MergeDocuments(outputPath, docPaths);//merge

                return outputPath;
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }

        void CreateWordDocument(string path, string text)
        {
            try
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
                {
                    // Add a main document part
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                    // Create the document structure
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());

                    // Add a title
                    WordHelper.AddTitle(body, text);

                    // Add some content paragraphs
                    WordHelper.AddParagraph(body, "This is a sample Word document created using DocumentFormat.OpenXml in C#.");
                    WordHelper.AddParagraph(body, "You can add multiple paragraphs, format text, insert tables, and much more.");

                    // Add a simple table
                    string[] headers = ["Name", "Age", "City"];
                    List<WordTest> data = new List<WordTest>
                    {
                        new WordTest()
                        {
                            Name="Test name",
                            Age=11,
                            City="test city"
                        },
                        new WordTest()
                        {
                            Name="Test name 1",
                            Age=12,
                            City="test city 1"
                        }
                    };
                    WordHelper.AddTable(body, headers, data);

                    // save in file
                    mainPart.Document.Save();

                    // Convert the workbook to a byte array
                    //using (var stream = new MemoryStream())
                    //{
                    //    mainPart.Document.Save(stream);
                    //    var fileBytes = stream.ToArray();
                    //    var r = File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "GeneratedDocument.docx");
                    //}
                }
            }
            catch (Exception ex)
            {
            }
        }

        [HttpPost("Dummy_ChangeInvalidPathOfDocuments")]
        public async Task<string> Dummy_ChangeInvalidPathOfDocuments()
        {
            try
            {
                CommonFuncationHelper.WriteTextLog("Dummy_ChangeInvalidPathOfDocuments");
                CommonFuncationHelper.WriteTextLog("start");
                //data
                var dt = await _unitOfWork.CommonFunctionRepository.Dummy_GetChangeInvalidPathOfDocuments();

                CommonFuncationHelper.WriteTextLog($"Data Table count: {dt?.Rows?.Count}");

                bool isFilePathCreated = false;
                bool isFileMoved = false;
                //copy the file and change the file name
                List<TestTwoPath> files = new List<TestTwoPath>();
                foreach (DataRow row in dt.Rows)
                {
                    //path
                    string oldPath = System.IO.Path.Combine(ConfigurationHelper.StaticFileRootPath, row["FolderPath"]?.ToString(), row["TransactionID"]?.ToString(), row["FileName"]?.ToString());

                    string newPath = System.IO.Path.Combine(ConfigurationHelper.StaticFileRootPath, row["FolderPath"]?.ToString(), row["TransactionID"]?.ToString(), row["FileName_new"]?.ToString());
                    //files
                    if (System.IO.File.Exists(oldPath))
                    {
                        isFilePathCreated = true;
                        files.Add(new TestTwoPath
                        {
                            OldPath = oldPath,
                            NewPath = newPath,
                        });
                    }
                }

                foreach (var file in files)
                {
                    try
                    {
                        if (System.IO.File.Exists(file.OldPath))
                        {
                            isFileMoved = true;

                            // Ensure the target directory exists
                            string newDirectory = System.IO.Path.GetDirectoryName(file.NewPath);
                            if (!Directory.Exists(newDirectory))
                            {
                                Directory.CreateDirectory(newDirectory);
                            }

                            // Open the source file with shared read access
                            using (var sourceStream = new FileStream(file.OldPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            using (var destinationStream = new FileStream(file.NewPath, FileMode.Create, FileAccess.Write))
                            {
                                await sourceStream.CopyToAsync(destinationStream);
                            }

                            CommonFuncationHelper.WriteTextLog($"Copied from {file.OldPath} to {file.NewPath}");
                        }
                    }
                    catch (Exception innerEx)
                    {
                        CommonFuncationHelper.WriteTextLog($"Failed to copy file: {file.OldPath} -> {file.NewPath}. Error: {innerEx.Message}");
                    }
                }


                CommonFuncationHelper.WriteTextLog("end");
                CommonFuncationHelper.WriteTextLog("Dummy_ChangeInvalidPathOfDocuments");

                return $"File Path Created: {isFilePathCreated}, File Moved: {isFileMoved}";
            }
            catch (Exception ex)
            {
                //log
                CommonFuncationHelper.WriteTextLog($"Error: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

        [HttpPost("Dummy_MoveFilesFromStudentFolderToNewFolderStructure")]
        public async Task<string> Dummy_MoveFilesFromStudentFolderToNewFolderStructure()
        {
            try
            {
                CommonFuncationHelper.WriteTextLog("Dummy_MoveFilesFromStudentFolderToNewFolderStructure");
                CommonFuncationHelper.WriteTextLog("start");


                var path = System.IO.Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder);
                var files = Directory.GetFiles(path);

                var list = new List<TestTwoPathNew>();
                foreach (var file in files)
                {
                    list.Add(new TestTwoPathNew
                    {
                        OldPath = file,
                        OldFileName = System.IO.Path.GetFileName(file)
                    });
                }

                //data
                var dt = await _unitOfWork.CommonFunctionRepository.Dummy_GetMoveFilesFromStudentFolderToNewFolderStructure(list);

                CommonFuncationHelper.WriteTextLog($"Data Table count: {dt?.Rows?.Count}");

                bool isAnyFileCopy = false;
                foreach (DataRow row in dt.Rows)
                {
                    string oldPath = row["OldPath"]?.ToString();
                    string newRelativePath = row["NewPath"]?.ToString();

                    if (string.IsNullOrEmpty(oldPath) || string.IsNullOrEmpty(newRelativePath))
                        continue;

                    string newPath = System.IO.Path.Combine(path, newRelativePath);

                    try
                    {
                        if (System.IO.File.Exists(oldPath))
                        {
                            isAnyFileCopy = true;

                            // Ensure the target directory exists
                            string newDirectory = System.IO.Path.GetDirectoryName(newPath);
                            if (!Directory.Exists(newDirectory))
                            {
                                Directory.CreateDirectory(newDirectory);
                            }

                            // Copy file to new location (overwrite if exists)
                            System.IO.File.Copy(oldPath, newPath, true);

                        }
                    }
                    catch (Exception innerEx)
                    {
                    }
                }


                CommonFuncationHelper.WriteTextLog("end");
                CommonFuncationHelper.WriteTextLog("Dummy_MoveFilesFromStudentFolderToNewFolderStructure");

                return $"File Moved: {isAnyFileCopy}";
            }
            catch (Exception ex)
            {
                //log
                CommonFuncationHelper.WriteTextLog($"Error: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

        [HttpPost("Test_SaveHindiData")]
        public async Task<string> Test_SaveHindiData(List<Test_SaveHindiData> model)
        {
            try
            {
                var result = await _unitOfWork.TestRepository.Test_SaveHindiData(model);
                _unitOfWork.SaveChanges();
                return true.ToString();
            }
            catch (Exception ex)
            {
                return false.ToString();
            }
        }

        [HttpGet("Dummy_SendMessage/{Type}")]
        public async Task<string> Dummy_SendMessage(string Type)
        {
            ActionName = "Dummy_SendMessage(string Type)";

            try
            {
                CommonFuncationHelper.WriteTextLog("Start: Dummy_SendMessage");

                string Description = "";
                string TempleteId = "";
                string MobileNo = "";

                var dataTable = await _unitOfWork.TestRepository.Dummy_SendMessage(Type);
                if (dataTable.Rows.Count == 0)
                {
                    throw new Exception("Data not found");
                }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Description = dataTable.Rows[i]["Description"].ToString();
                    TempleteId = dataTable.Rows[i]["TempleteId"].ToString();
                    MobileNo = dataTable.Rows[i]["MobileNo"].ToString();

                    try
                    {
                        //send
                        var res = CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, MobileNo, Description, TempleteId);
                    }
                    catch (Exception ex)
                    {
                        CommonFuncationHelper.WriteTextLog($"Error: {ex.Message}, Mobile: {MobileNo}");
                    }
                }

                CommonFuncationHelper.WriteTextLog("End: Dummy_SendMessage");

                return "Sent successfully.";
            }
            catch (Exception ex)
            {
                CommonFuncationHelper.WriteTextLog($"Error: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

    }
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile file { get; set; }
        public List<A> a { get; set; }
    }
    public class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class WordTest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
    public class TestTwoPath
    {
        public string OldPath { get; set; }
        public string NewPath { get; set; }
    }

}
