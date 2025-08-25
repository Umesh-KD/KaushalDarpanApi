using System;
using System.Text;
using System.Text.RegularExpressions;

public class UnicodeToKrutidev
{
    private static string fontFamily = "font-family: Kruti Dev 010;";

    private static string[] array_one = new string[] {
        "‘", "’", "“", "”", "(", ")", "{", "}", "=", "।", "?", "-", "µ", "॰", ",", ".", "् ",
        "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", "x",
        "फ़्", "क़", "ख़", "ग़", "ज़्", "ज़", "ड़", "ढ़", "फ़", "य़", "ऱ", "ऩ",
        "त्त्", "त्त", "क्त", "दृ", "कृ",
        "ह्न", "ह्य", "हृ", "ह्म", "ह्र", "ह्", "द्द", "क्ष्", "क्ष", "त्र्", "त्र", "ज्ञ",
        "छ्य", "ट्य", "ठ्य", "ड्य", "ढ्य", "द्य", "द्व",
        "श्र", "ट्र", "ड्र", "ढ्र", "छ्र", "क्र", "फ्र", "द्र", "प्र", "ग्र", "रु", "रू",
        "्र",
        "ओ", "औ", "आ", "अ", "ई", "इ", "उ", "ऊ", "ऐ", "ए", "ऋ",
        "क्", "क", "क्क", "ख्", "ख", "ग्", "ग", "घ्", "घ", "ङ",
        "चै", "च्", "च", "छ", "ज्", "ज", "झ्", "झ", "ञ",
        "ट्ट", "ट्ठ", "ट", "ठ", "ड्ड", "ड्ढ", "ड", "ढ", "ण्", "ण",
        "त्", "त", "थ्", "थ", "द्ध", "द", "ध्", "ध", "न्", "न",
        "प्", "प", "फ्", "फ", "ब्", "ब", "भ्", "भ", "म्", "म",
        "य्", "य", "र", "ल्", "ल", "ळ", "व्", "व",
        "श्", "श", "ष्", "ष", "स्", "स", "ह",
        "ऑ", "ॉ", "ो", "ौ", "ा", "ी", "ु", "ू", "ृ", "े", "ै",
        "ं", "ँ", "ः", "ॅ", "ऽ", "् ", "्", "ि"
    };

    private static string[] array_two = new string[] {
        "^", "*", "Þ", "ß", "¼", "½", "¿", "À", "¾", "A", "\\", "&", "&", "Œ", "]", "-", "~ ",
        "å", "ƒ", "„", "…", "†", "‡", "ˆ", "‰", "Š", "‹", "Û",
        "¶", "d", "[k", "x", "T", "t", "M+", "<+", "Q", ";", "j", "u",
        "Ù", "Ùk", "Dr", "–", "—",
        "à", "á", "â", "ã", "ºz", "º", "í", "{", "{k", "«", "=", "K",
        "Nî", "Vî", "Bî", "Mî", "<î", "|", "}",
        "J", "Vª", "Mª", "<ªª", "Nª", "Ø", "Ý", "æ", "ç", "xz", "#", ":",
        "z",
        "vks", "vkS", "vk", "v", "bZ", "b", "m", "Å", ",s", ",", "_",
        "D", "d", "ô", "[", "[k", "X", "x", "?", "?k", "³",
        "pkS", "P", "p", "N", "T", "t", "÷", ">", "¥",
        "ê", "ë", "V", "B", "ì", "ï", "M", "<", ".", ".k",
        "R", "r", "F", "Fk", ")", "n", "/", "/k", "U", "u",
        "I", "i", "¶", "Q", "C", "c", "H", "Hk", "E", "e",
        "¸", ";", "j", "Y", "y", "G", "O", "o",
        "'", "'k", "⚡", "⚡k", "L", "l", "g",
        "v‚", "‚", "ks", "kS", "k", "h", "q", "w", "`", "s", "S",
        "a", "¡", "%", "W", "·", "~ ", "~", ""
    };

    public static string UnicodeToKrutiDev(string unicode_substring)
    {
        int array_one_length = array_one.Length;
        string modified_substring = unicode_substring;

        // Replace single quotes (') alternately with ^ and *
        int position_of_quote = modified_substring.IndexOf("'");
        while (position_of_quote >= 0)
        {
            modified_substring = ReplaceFirstOccurrence(modified_substring, "'", "^");
            modified_substring = ReplaceFirstOccurrence(modified_substring, "'", "*");
            position_of_quote = modified_substring.IndexOf("'");
        }

        // Replace double quotes (") alternately with ß and Þ
        int position_of_Dquote = modified_substring.IndexOf("\"");
        while (position_of_Dquote >= 0)
        {
            modified_substring = ReplaceFirstOccurrence(modified_substring, "\"", "ß");
            modified_substring = ReplaceFirstOccurrence(modified_substring, "\"", "Þ");
            position_of_Dquote = modified_substring.IndexOf("\"");
        }

        // Replace nukta characters to composed forms
        modified_substring = modified_substring.Replace("क़", "क़");
        modified_substring = modified_substring.Replace("ख़", "ख़");
        modified_substring = modified_substring.Replace("ग़", "ग़");
        modified_substring = modified_substring.Replace("ज़", "ज़");
        modified_substring = modified_substring.Replace("ड़", "ड़");
        modified_substring = modified_substring.Replace("ढ़", "ढ़");
        modified_substring = modified_substring.Replace("ऩ", "ऩ");
        modified_substring = modified_substring.Replace("फ़", "फ़");
        modified_substring = modified_substring.Replace("य़", "य़");
        modified_substring = modified_substring.Replace("ऱ", "ऱ");
        modified_substring = modified_substring.Replace("क्‍त", "क्त");
        modified_substring = modified_substring.Replace("ढ़", "ढ");
        modified_substring = modified_substring.Replace("ढ", "ड़");
        modified_substring = modified_substring.Replace("ढ़", "ड़");
        modified_substring = modified_substring.Replace("़", "");

        // Handle "ि" (chhoti ee ki matra) repositioning with "f"
        var position_of_f = modified_substring.IndexOf("ि");
        while (position_of_f != -1)
        {
            var character_left_to_f = modified_substring[position_of_f - 1];
            modified_substring = modified_substring.Replace(character_left_to_f + "ि", "f" + character_left_to_f);

            position_of_f = position_of_f - 1;

            while ((position_of_f != 0) && (modified_substring[position_of_f - 1] == '्'))
            {
                var string_to_be_Replaced = modified_substring.Substring(position_of_f - 2, 2);
                modified_substring = modified_substring.Replace(string_to_be_Replaced + "f", "f" + string_to_be_Replaced);

                position_of_f = position_of_f - 2;
            }
            position_of_f = modified_substring.IndexOf("ि", position_of_f + 1);
        }
        modified_substring = RepositionChhotiEeMatra(modified_substring);

        // Move "र्" (half-R) to correct position
        string set_of_matras = "ािीुूृेैोौं:ँॅ";
        modified_substring += "   "; // padding to prevent index out of range

        int pos = modified_substring.IndexOf("र्");
        while (pos != -1 && pos < modified_substring.Length - 1)
        {
            int start = pos;
            int current = pos + 2; // skip "र्"

            // Skip over consonants joined with halant (e.g. conjuncts like "ट्+ण")
            while (current + 1 < modified_substring.Length &&
                   modified_substring[current + 1] == '्')
            {
                current += 2;
            }

            // Skip over matras
            while (current < modified_substring.Length &&
                   set_of_matras.IndexOf(modified_substring[current]) != -1)
            {
                current++;
            }

            // Construct parts
            string reph = "र्";
            string before = modified_substring.Substring(0, start);
            string middle = modified_substring.Substring(start + 2, current - start - 2);
            string after = modified_substring.Substring(current);

            modified_substring = before + middle + reph + after;

            pos = modified_substring.IndexOf("र्", pos + 1);
        }

        modified_substring = modified_substring.Trim();
        //modified_substring = HandleReph(modified_substring);
        // Replace using mapping arrays
        for (int input_symbol_idx = 0; input_symbol_idx < array_one_length; input_symbol_idx++)
        {
            modified_substring = modified_substring.Replace(array_one[input_symbol_idx], array_two[input_symbol_idx]);
        }


        modified_substring = MoveJTildeToZ(modified_substring);

        modified_substring = modified_substring.Replace("⚡", "\"");

        return modified_substring;
    }


    private static string MoveJTildeToZ(string input)
    {
        //input = input.Replace("\", "");
        input = @input.Replace("\\", "");
        StringBuilder result = new StringBuilder(input);
        int searchStart = 0;

      

        while (true)
        {
            int index = result.ToString().IndexOf("j~", searchStart);
            if (index == -1)
                break;

            // Remove "j~"
            result.Remove(index, 2);

            // Calculate new position for Z
           
            int insertPos = Math.Min(index + 2, result.Length);
            if (input.IndexOf("⚡") > 0)
            {
                insertPos += 1;
            }

            // Insert 'Z'
            result.Insert(insertPos, 'Z');

            // Update search start position
            searchStart = insertPos + 1;
        }

        return result.ToString();
    }


    public static string GetKrutidev(string Text)
    {
        return FindAndReplaceKrutidev(" " + Text + " ", false).TrimEnd().TrimStart().Replace("  ", " ");
    }

    public static string FindAndReplaceKrutidev(string Text, bool IsApplyFontSize = true, string DefaultFontSize = "20px")
    {
        //string fontFamily = "font-family: Kruti Dev 010;";
        string modified_substring = Text;
        modified_substring = modified_substring.Replace(">", "> ");//.Replace("ढ़", "ढ");
        modified_substring = modified_substring.Replace("<", " <");
        //modified_substring = modified_substring.Replace("़", "");

        modified_substring = modified_substring.Replace("क़", "क़");
        modified_substring = modified_substring.Replace("ख़", "ख़");
        modified_substring = modified_substring.Replace("ग़", "ग़");
        modified_substring = modified_substring.Replace("ज़", "ज़");
        modified_substring = modified_substring.Replace("ड़", "ड़");
        modified_substring = modified_substring.Replace("ढ़", "ढ़");
        modified_substring = modified_substring.Replace("ऩ", "ऩ");
        modified_substring = modified_substring.Replace("फ़", "फ़");
        modified_substring = modified_substring.Replace("य़", "य़");
        modified_substring = modified_substring.Replace("ऱ", "ऱ");
        modified_substring = modified_substring.Replace("क्‍त", "क्त");
        modified_substring = modified_substring.Replace("़", "");





        modified_substring = modified_substring.Replace("/", " / ");
        modified_substring = modified_substring.Replace("(", " ( ");
        modified_substring = modified_substring.Replace(")", " ) ");



        string htmlEditor = "";

        System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
        htmlEditor = rx.Replace(Text, " ");


        htmlEditor = htmlEditor.Replace("क़", "क़");
        htmlEditor = htmlEditor.Replace("ख़", "ख़");
        htmlEditor = htmlEditor.Replace("ग़", "ग़");
        htmlEditor = htmlEditor.Replace("ज़", "ज़");
        htmlEditor = htmlEditor.Replace("ड़", "ड़");
        htmlEditor = htmlEditor.Replace("ढ़", "ढ़");
        htmlEditor = htmlEditor.Replace("ऩ", "ऩ");
        htmlEditor = htmlEditor.Replace("फ़", "फ़");
        htmlEditor = htmlEditor.Replace("य़", "य़");
        htmlEditor = htmlEditor.Replace("ऱ", "ऱ");
        htmlEditor = htmlEditor.Replace("क्‍त", "क्त");
        htmlEditor = htmlEditor.Replace("़", "");

        htmlEditor = htmlEditor.Replace("/", " / ");
        htmlEditor = htmlEditor.Replace("(", " ( ");
        htmlEditor = htmlEditor.Replace(")", " ) ");



        string[] txtArr = htmlEditor.Replace(">", "> ").Replace("<", " <").Replace("  ", " ").Split(' ');
        for (int i = 0; i < txtArr.Length; i++)
        {
            int x = 0;

            for (int j = 0; j < txtArr[i].Length; j++)
            {
                if (Array.IndexOf(array_one, txtArr[i][j].ToString()) > 0)
                {
                    //string txt = ;
                    ++x;
                }
            }
            if (x == txtArr[i].Length && x > 0)
                modified_substring = modified_substring.Replace(" " + txtArr[i].ToString() + " ", " <label style='" + (IsApplyFontSize ? "font-size:" + DefaultFontSize + ";" : "") + fontFamily + "'> " + UnicodeToKrutiDev(txtArr[i]) + " </label> ");

        }
        modified_substring = modified_substring.Replace(" / ", "/");
        modified_substring = modified_substring.Replace(" ( ", "(");
        modified_substring = modified_substring.Replace(" ) ", ")");
        return modified_substring;
    }
    private static string RepositionChhotiEeMatra(string input)
    {
        var output = new System.Text.StringBuilder();
        int i = 0;

        while (i < input.Length)
        {
            if (i + 1 < input.Length && input[i + 1] == 'ि')
            {
                // Move "ि" before consonant cluster starting at i
                // Find start of consonant cluster (handle halant '्')

                int start = i;
                while (start > 0 && input[start - 1] == '्')
                {
                    start -= 2; // move back over consonant + halant
                }

                // Append everything before start
                output.Append(input.Substring(0, start));

                // Append 'f' which is Kruti Dev symbol for "ि"
                output.Append('f');

                // Append consonant cluster
                output.Append(input.Substring(start, i + 1 - start));

                // Cut processed part from input
                input = input.Substring(i + 2);

                // Reset i and continue with new input
                i = 0;
            }
            else
            {
                i++;
            }
        }

        // Append any remaining characters
        output.Append(input);

        return output.ToString();
    }


    private static string HandleReph(string input)
    {
        // Reph (र्) represented by 'Z' or similar markers in Kruti Dev
        return Regex.Replace(input, "Z([क-ह][्]?)", "र्$1");
    }

    private static string HandleChhotiEe(string input)
    {
        // Move 'ि' after the consonant in Kruti Dev to before consonant in Unicode
        return Regex.Replace(input, "([क-ह]्?)(ि)", "ि$1");
    }

    private static string ReplaceFirstOccurrence(string source, string find, string replace)
    {
        int place = source.IndexOf(find);
        if (place == -1)
            return source;
        return source.Substring(0, place) + replace + source.Substring(place + find.Length);
    }

    public static class UnicodeToKrutiDevConverter
    {
        static readonly string[] array_from = new string[] {
        "‘", "’", "“", "”", "(", ")", "{", "}", "=", "।", "?", "-", "µ", "॰", ",", ".", "् ", "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", "x", "फ़्", "क़", "ख़", "ग़", "ज़्", "ज़", "ड़", "ढ़", "फ़", "य़", "ऱ", "ऩ", "त्त्", "त्त", "क्त", "दृ", "कृ", "ह्न", "ह्य", "हृ", "ह्म", "ह्र", "ह्", "द्द", "क्ष्", "क्ष", "त्र्", "त्र", "ज्ञ", "छ्य", "ट्य", "ठ्य", "ड्य", "ढ्य", "द्य", "द्व", "श्र", "ट्र", "ड्र", "ढ्र", "छ्र", "क्र", "फ्र", "द्र", "प्र", "ग्र", "रु", "रू", "्र", "ओ", "औ", "आ", "अ", "ई", "इ", "उ", "ऊ", "ऐ", "ए", "ऋ", "क्", "क", "क्क", "ख्", "ख", "ग्", "ग", "घ्", "घ", "ङ", "चै", "च्", "च", "छ", "ज्", "ज", "झ्", "झ", "ञ", "ट्ट", "ट्ठ", "ट", "ठ", "ड्ड", "ड्ढ", "ड", "ढ", "ण्", "ण", "त्", "त", "थ्", "थ", "द्ध", "द", "ध्", "ध", "न्", "न", "प्", "प", "फ्", "फ", "ब्", "ब", "भ्", "भ", "म्", "म", "य्", "य", "र", "ल्", "ल", "ळ", "व्", "व", "श्", "श", "ष्", "ष", "स्", "स", "ह", "ऑ", "ॉ", "ो", "ौ", "ा", "ी", "ु", "ू", "ृ", "े", "ै", "ं", "ँ", "ः", "ॅ", "ऽ", "् ", "्"
    };

        static readonly string[] array_to = new string[] {
        "^", "*", "Þ", "ß", "¼", "½", "¿", "À", "¾", "A", "\\", "&", "&", "Œ", "]", "-", "~ ", "å", "ƒ", "„", "…", "†", "‡", "ˆ", "‰", "Š", "‹", "Û", "¶", "d", "[k", "x", "T", "t", "M+", "<+", "Q", ";", "j", "u", "Ù", "Ùk", "ä", "–", "—", "à", "á", "â", "ã", "ºz", "º", "í", "{", "{k", "«", "=", "K", "Nî", "Vî", "Bî", "Mî", "<î", "|", "}", "J", "Vª", "Mª", "<ªª", "Nª", "Ø", "Ý", "æ", "ç", "xz", "#", ":", "z", "vks", "vkS", "vk", "v", "bZ", "b", "m", "Å", ",s", ",", "_", "D", "d", "ô", "[", "[k", "X", "x", "?", "?k", "³", "pkS", "P", "p", "N", "T", "t", "÷", ">", "¥", "ê", "ë", "V", "B", "ì", "ï", "M", "<", ".", ".k", "R", "r", "F", "Fk", ")", "n", "/", "/k", "U", "u", "I", "i", "¶", "Q", "C", "c", "H", "Hk", "E", "e", "¸", ";", "j", "Y", "y", "G", "O", "o", "'", "'k", "\"", "\"k", "L", "l", "g", "v‚", "‚", "ks", "kS", "k", "h", "q", "w", "`", "s", "S", "a", "¡", "%", "W", "·", "~ ", "~"
    };

        public static string ConvertUnicodeToKruti(string input)
        {
            string text = input;

            // Replace composite characters
            text = text.Replace("क़", "क़").Replace("ख़", "ख़").Replace("ग़", "ग़").Replace("ज़", "ज़").Replace("ड़", "ड़")
                       .Replace("ढ़", "ढ़").Replace("ऩ", "ऩ").Replace("फ़", "फ़").Replace("य़", "य़").Replace("ऱ", "ऱ");

            // Move 'ि' before consonants
            int position = text.IndexOf("ि");
            while (position > 0)
            {
                var preceding = text[position - 1];
                string temp = preceding + "ि";
                text = text.Replace(temp, "f" + preceding);

                int back = position - 1;
                while (back > 1 && text[back - 1] == '्')
                {
                    string cluster = text.Substring(back - 2, 2);
                    text = text.Replace(cluster + "f", "f" + cluster);
                    back -= 2;
                }

                position = text.IndexOf("ि", position + 1);
            }

            // Move 'र्' to the right and replace with 'Z'
            string matras = "ािीुूृेैोौं:ँॅ";
            text += "  ";
            int halfRIndex = text.IndexOf("र्");

            while (halfRIndex > 0)
            {
                int probableZ = halfRIndex + 2;
                char nextChar = text[probableZ + 1];
                while (matras.IndexOf(nextChar) != -1)
                {
                    probableZ++;
                    nextChar = text[probableZ + 1];
                }

                string toReplace = text.Substring(halfRIndex, probableZ - halfRIndex + 1);
                string replacement = text.Substring(halfRIndex + 2, probableZ - halfRIndex - 1) + "Z";
                text = text.Replace(toReplace, replacement);
                halfRIndex = text.IndexOf("र्");
            }

            text = text.Substring(0, text.Length - 2);

            // Replace characters from array
            for (int i = 0; i < array_from.Length; i++)
            {
                while (text.Contains(array_from[i]))
                {
                    text = text.Replace(array_from[i], array_to[i]);
                }
            }

            return text;
        }
    }

}
