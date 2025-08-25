using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.GroupCodeAllocation;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class GroupCodeAllocationController : BaseController
    {
        public override string PageName => "GroupCodeAllocationController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GroupCodeAllocationController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<GroupCodeAllocationAddEditModel>>> GetAllData([FromBody] GroupCodeAllocationSearchModel filterModel)
        {
            ActionName = "GetAllData([FromBody] GroupCodeAllocationSearchModel filterModel)";
            var result = new ApiResult<List<GroupCodeAllocationAddEditModel>>();
            try
            {
                // Pass the entire model to the repository
                var data = await _unitOfWork.GroupCodeAllocationRepository.GetAllData(filterModel);

                if (data != null)
                {
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                _unitOfWork.Dispose();
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }

        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] List<GroupCodeAllocationAddEditModel> request, int StartValue)
        {
            ActionName = "SaveData([FromBody] List<GroupCodeAllocationAddEditModel> request, int StartValue)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    if (request?.Count == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.GroupCodeAllocationRepository.SaveData(request, StartValue);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_UPDATE_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                }
                return result;
            });
        }

        [HttpPost("GetPartitionData")]
        public async Task<ApiResult<List<GroupCodeAddEditModel>>> GetPartitionData([FromBody] GroupCodeAllocationSearchModel filterModel)
        {
            ActionName = "GetAllData([FromBody] GroupCodeAllocationSearchModel filterModel)";
            var result = new ApiResult<List<GroupCodeAddEditModel>>();
            try
            {
                if (filterModel.PartitionSize <= 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_VALIDATION_FAILED;
                    return result;
                }
                // Pass the entire model to the repository
                var data = await _unitOfWork.GroupCodeAllocationRepository.GetPartitionData(filterModel);
                data = SetThenOptimizePartitionData(data, filterModel.PartitionSize);// set and optimize group code data

                if (data?.Count > 0)
                {
                    //result.Data = data;
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.Data = [];
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                _unitOfWork.Dispose();
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }

        [HttpPost("SavePartitionData")]
        public async Task<ApiResult<bool>> SavePartitionData([FromBody] List<GroupCodeAddEditModel> request)
        {
            ActionName = "SavePartitionData([FromBody] List<GroupCodeAddEditModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    if (request == null)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.GroupCodeAllocationRepository.SavePartitionData(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                    else if (isSave == 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_RECORD_ALREADY_EXISTS;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_UPDATE_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                }
                return result;
            });
        }


        #region private function if need
        private List<GroupCodeAddEditModel> SetThenOptimizePartitionData(List<GroupCodeAddEditModel> studentPapers, int PartitionSize)
        {
            var newList = new List<GroupCodeAddEditModel>();

            //loop on that not direct picked (papercount>=PartitionSize)
            var distinctSubjectCodes = studentPapers.Where(x => x.IsDirectPicked == false).DistinctBy(x => x.SubjectCode).Select(x => x.SubjectCode).ToList();

            //each subject code 
            foreach (var distinctSubjectCode in distinctSubjectCodes)
            {
                // get all subject code for selected
                var subjectStudentPapers = studentPapers.Where(x => x.IsDirectPicked == false && x.SubjectCode == distinctSubjectCode).ToList();

                //loop for selected subject code
                foreach (var subjectStudentPaper in subjectStudentPapers)
                {
                    var mergedRows = new List<string>();

                    // get all group rno that already merged
                    var mergedGroupNo_StudentExamPaperMarksIDs = newList
                                                                    .Where(x => x.SubjectCode == distinctSubjectCode)
                                                                    .SelectMany(x => x.StudentExamPaperMarksIDs.Split(','))
                                                                    .ToList();
                    //current
                    int currentPaperCount = subjectStudentPaper.Total;//set current
                    var currentStudentExamPaperMarksIDs = subjectStudentPaper.StudentExamPaperMarksIDs.Split(',')
                                                .Where(x => mergedGroupNo_StudentExamPaperMarksIDs
                                                            .Contains(x) == false)
                                                .ToList();
                    mergedRows.AddRange(currentStudentExamPaperMarksIDs);//add current

                    // get all subject code for selected but not already merged
                    var subjectNotAlreadyMergedStudentPapers = subjectStudentPapers
                                                .Where(x => x.StudentExamPaperMarksIDs.Split(',')
                                                                .All(id => mergedGroupNo_StudentExamPaperMarksIDs.Contains(id) == false)
                                                           && x.StudentExamPaperMarksIDs.Split(',')
                                                                .All(id => mergedRows.Contains(id) == false))
                                                .ToList();

                    //check further added
                    foreach (var subjectNotAlreadyMergedStudentPaper in subjectNotAlreadyMergedStudentPapers)
                    {
                        currentPaperCount += subjectNotAlreadyMergedStudentPaper.Total;
                        if (currentPaperCount <= PartitionSize)
                        {
                            mergedRows.AddRange(subjectNotAlreadyMergedStudentPaper.StudentExamPaperMarksIDs.Split(','));
                        }
                    }

                    //add in new list
                    if (mergedRows.Count > 0)
                    {
                        var singleSP = new GroupCodeAddEditModel();

                        //current but not already merged
                        var subjectStudentPapersMergeds = subjectStudentPapers
                                                                .Where(x => x.StudentExamPaperMarksIDs.Split(',')
                                                                            .All(id => mergedRows.Contains(id) == true))
                                                                .ToList();
                        if (subjectStudentPapersMergeds.Count > 0)
                        {
                            foreach (var subjectStudentPapersMerged in subjectStudentPapersMergeds)
                            {
                                singleSP.GroupNo = subjectStudentPapersMerged.GroupNo;
                                singleSP.PageNumber = subjectStudentPapersMerged.PageNumber;
                                singleSP.SemesterID = subjectStudentPapersMerged.SemesterID;
                                singleSP.SemesterName = subjectStudentPapersMerged.SemesterName;
                                singleSP.StudentExamPaperMarksIDs += subjectStudentPapersMerged.StudentExamPaperMarksIDs + ",";//merge
                                singleSP.Total += subjectStudentPapersMerged.Total;//merge
                                singleSP.SubjectCode = subjectStudentPapersMerged.SubjectCode;
                                singleSP.CommonSubjectID = subjectStudentPapersMerged.CommonSubjectID;
                                singleSP.CommonSubjectName = subjectStudentPapersMerged.CommonSubjectName;
                                singleSP.DepartmentID = subjectStudentPapersMerged.DepartmentID;
                                singleSP.Eng_NonEng = subjectStudentPapersMerged.Eng_NonEng;
                                singleSP.EndTermID = subjectStudentPapersMerged.EndTermID;
                                singleSP.CenterCode = subjectStudentPapersMerged.CenterCode;
                            }
                            //remove last comma
                            singleSP.StudentExamPaperMarksIDs = singleSP.StudentExamPaperMarksIDs.Trim().TrimEnd(',');
                            //add in new list
                            newList.Add(singleSP);
                        }
                    }
                }
            }

            //get direct picked (papercount>=PartitionSize)
            var directPickedStudents = studentPapers.Where(x => x.IsDirectPicked == true).ToList();
            newList.AddRange(directPickedStudents);

            //sort
            var orderedList = newList
                                .OrderBy(x => x.SubjectCode)
                                .ThenBy(x => x.CommonSubjectID)
                                .ToList();
            //set new pagenumber
            string? currentSubject = null;
            int pageno = 1;

            foreach (var item in orderedList)
            {
                if (item.SubjectCode != currentSubject)
                {
                    currentSubject = item.SubjectCode;
                    pageno = 1;
                }
                // set
                item.PartitionSize = PartitionSize;
                item.PageNumber = pageno++;

                // if any subject group has more then 1 row
                var subjectGorupCount = orderedList.Count(x => x.SubjectCode == currentSubject);
                if (subjectGorupCount > 1)
                {
                    item.UpShiftPageNumber = item.PageNumber;
                }
            }

            return orderedList;
        }
        #endregion
    }
}


