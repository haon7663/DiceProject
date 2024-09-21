using System;

public static class StringUtils
{
    private const string KOREA_JONGSUNG1 = "을(를)";
    private const string KOREA_JONGSUNG2 = "이(가)";
    private const string KOREA_JONGSUNG3 = "은(는)";
    private const string KOREA_JONGSUNG4 = "과(와)";
    private const string KOREA_JONGSUNG5 = "으로(로)";

    private const string KOREA_JONGSUNG1_VALUE1 = "을";
    private const string KOREA_JONGSUNG1_VALUE2 = "를";
    private const string KOREA_JONGSUNG2_VALUE1 = "이";
    private const string KOREA_JONGSUNG2_VALUE2 = "가";
    private const string KOREA_JONGSUNG3_VALUE1 = "은";
    private const string KOREA_JONGSUNG3_VALUE2 = "는";
    private const string KOREA_JONGSUNG4_VALUE1 = "과";
    private const string KOREA_JONGSUNG4_VALUE2 = "와";
    private const string KOREA_JONGSUNG5_VALUE1 = "으로";
    private const string KOREA_JONGSUNG5_VALUE2 = "로";


    public static string ConvertKoreaStringJongSung(this string koreaString)
    {
        if (koreaString.Contains(KOREA_JONGSUNG1))
        {
            koreaString = ConvertKoreaStringJongSung(koreaString, KOREA_JONGSUNG1, KOREA_JONGSUNG1_VALUE1,
                KOREA_JONGSUNG1_VALUE2);
        }

        if (koreaString.Contains(KOREA_JONGSUNG2))
        {
            koreaString = ConvertKoreaStringJongSung(koreaString, KOREA_JONGSUNG2, KOREA_JONGSUNG2_VALUE1,
                KOREA_JONGSUNG2_VALUE2);
        }

        if (koreaString.Contains(KOREA_JONGSUNG3))
        {
            koreaString = ConvertKoreaStringJongSung(koreaString, KOREA_JONGSUNG3, KOREA_JONGSUNG3_VALUE1,
                KOREA_JONGSUNG3_VALUE2);
        }

        if (koreaString.Contains(KOREA_JONGSUNG4))
        {
            koreaString = ConvertKoreaStringJongSung(koreaString, KOREA_JONGSUNG4, KOREA_JONGSUNG4_VALUE1,
                KOREA_JONGSUNG4_VALUE2);
        }
        
        if (koreaString.Contains(KOREA_JONGSUNG5))
        {
            koreaString = ConvertKoreaStringJongSung(koreaString, KOREA_JONGSUNG5, KOREA_JONGSUNG5_VALUE1, KOREA_JONGSUNG5_VALUE2);
        }

        return koreaString;
    }

    public static string ConvertKoreaStringJongSung(string koreaString, string check, string first, string second)
    {
        int emptyCheck = 1;
        string fullString = string.Empty;
        string[] result = koreaString.Split(new string[] { check }, StringSplitOptions.None);
        if (result.Length >= 2)
        {
            for (int i = 0; i < result.Length - 1; i++)
            {
                emptyCheck = 1;
                if (result[i].Length > 0)
                {
                    char[] fullChar = result[i].ToCharArray(0, result[i].Length);
                    if (fullChar.Length > 0)
                    {
                        if (fullChar[fullChar.Length - 1] == ' ')
                        {
                            emptyCheck = 2;
                        }
                    }

                    char[] lastName = result[i].ToCharArray(result[i].Length - emptyCheck, 1);
                    if (lastName.Length > 0)
                    {
                        if (lastName[0] >= 0xAC00 && lastName[0] <= 0xD7A3)
                        {
                            String seletedValue = (lastName[0] - 0xAC00) % 28 > 0
                                ? first
                                : second;
                            result[i] = result[i] + seletedValue;
                        }
                        //숫자에 따라 조사 변화
                        else if (lastName[0] == 0x0030 || lastName[0] == 0x0031 || lastName[0] == 0x0033 ||
                                 lastName[0] == 0x0036 || lastName[0] == 0x0037 ||
                                 lastName[0] == 0x0038) //0, 1, 3, 6, 7, 8
                        {
                            result[i] = result[i] + first;
                        }
                        else if (lastName[0] == 0x0032 || lastName[0] == 0x0034 || lastName[0] == 0x0035 ||
                                 lastName[0] == 0x0039) //2, 4, 5, 9
                        {
                            result[i] = result[i] + second;
                        }
                    }
                }

                fullString += result[i];
            }

            fullString += result[result.Length - 1];
            koreaString = fullString;
        }

        return koreaString;
    }
}