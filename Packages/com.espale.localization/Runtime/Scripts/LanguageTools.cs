using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Espale.Localization
{
    public static class LanguageTools
    {
        /// <summary>
        /// Returns the ISO code of the given language
        /// </summary>
        /// <param name="lang">language to get the iso code of.</param>
        /// <returns>ISO code of the given language, null if not available.</returns>
        public static string IsoCodeFromLanguage(Language lang)
        {
            return languageToIsoCodeMap[lang];
        }
        
        /// <summary>
        /// Returns the the language associated with the given ISO code
        /// </summary>
        /// <param name="isoCode">ISO code of the language</param>
        /// <returns>language, associated with the given ISO code.</returns>
        public static Language LanguageFromIsoCode(string isoCode)
        {
            return languageToIsoCodeMap.FirstOrDefault(pair => pair.Value == isoCode.ToLower()).Key;
        }

        /// <summary>
        /// Maps the given SystemLanguage to the Language enum of Espale.Localization.Language enum
        /// Useful for auto-setting the language from the system language.
        /// </summary>
        /// <param name="systemLanguage"></param>
        /// <returns></returns>
        public static Language LanguageFromSystemLanguage(SystemLanguage systemLanguage)
        {
            return systemLanguage switch
            {
                SystemLanguage.Afrikaans => Language.Afrikaans,
                SystemLanguage.Arabic => Language.Arabic,
                SystemLanguage.Basque => Language.Basque,
                SystemLanguage.Belarusian => Language.Belarusian,
                SystemLanguage.Bulgarian => Language.Bulgarian,
                SystemLanguage.Catalan => Language.Catalan,
                SystemLanguage.Chinese => Language.Chinese,
                SystemLanguage.Czech => Language.Czech,
                SystemLanguage.Danish => Language.Danish,
                SystemLanguage.Dutch => Language.Dutch,
                SystemLanguage.English => Language.English,
                SystemLanguage.Estonian => Language.Estonian,
                SystemLanguage.Faroese => Language.Faroese,
                SystemLanguage.Finnish => Language.Finnish,
                SystemLanguage.French => Language.French,
                SystemLanguage.German => Language.German,
                SystemLanguage.Greek => Language.Greek,
                SystemLanguage.Hebrew => Language.Hebrew,
                SystemLanguage.Hungarian => Language.Hungarian,
                SystemLanguage.Icelandic => Language.Icelandic,
                SystemLanguage.Indonesian => Language.Indonesian,
                SystemLanguage.Italian => Language.Italian,
                SystemLanguage.Japanese => Language.Japanese,
                SystemLanguage.Korean => Language.Korean,
                SystemLanguage.Latvian => Language.Latvian,
                SystemLanguage.Lithuanian => Language.Lithuanian,
                SystemLanguage.Norwegian => Language.Norwegian,
                SystemLanguage.Polish => Language.Polish,
                SystemLanguage.Portuguese => Language.Portuguese,
                SystemLanguage.Romanian => Language.Romanian,
                SystemLanguage.Russian => Language.Russian,
                SystemLanguage.SerboCroatian => Language.Serbian,
                SystemLanguage.Slovak => Language.Slovak,
                SystemLanguage.Slovenian => Language.Slovenian,
                SystemLanguage.Spanish => Language.Spanish,
                SystemLanguage.Swedish => Language.Swedish,
                SystemLanguage.Thai => Language.Thai,
                SystemLanguage.Turkish => Language.Turkish,
                SystemLanguage.Ukrainian => Language.Ukrainian,
                SystemLanguage.Vietnamese => Language.Vietnamese,
                SystemLanguage.ChineseSimplified => Language.ChineseSimplified,
                SystemLanguage.ChineseTraditional => Language.Chinese,
                SystemLanguage.Hindi => Language.Hindi,
                _ => Language.Unknown
            };
        }
        
        internal static Dictionary<Language, string> languageToIsoCodeMap = new ()
        {
            { Language.Abkhazian, "ab" },
            { Language.Afar, "aa" },
            { Language.Afrikaans, "af" },
            { Language.Akan, "ak" },
            { Language.Albanian, "sq" },
            { Language.Amharic, "am" },
            { Language.Arabic, "ar" },
            { Language.Aragonese, "an" },
            { Language.Armenian, "hy" },
            { Language.Assamese, "as" },
            { Language.Avaric, "av" },
            { Language.Avestan, "ae" },
            { Language.Aymara, "ay" },
            { Language.Azerbaijani, "az" },
            { Language.Bambara, "bm" },
            { Language.Bashkir, "ba" },
            { Language.Basque, "eu" },
            { Language.Belarusian, "be" },
            { Language.Bengali, "bn" },
            { Language.Bislama, "bi" },
            { Language.Bosnian, "bs" },
            { Language.Breton, "br" },
            { Language.Bulgarian, "bg" },
            { Language.Burmese, "my" },
            { Language.Catalan, "ca" },
            { Language.Chamorro, "ch" },
            { Language.Chechen, "ce" },
            { Language.Chichewa, "ny" },
            { Language.Chinese, "zh" },
            { Language.ChurchSlavonic, "cu" },
            { Language.Chuvash, "cv" },
            { Language.Cornish, "kw" },
            { Language.Corsican, "co" },
            { Language.Cree, "cr" },
            { Language.Croatian, "hr" },
            { Language.Czech, "cs" },
            { Language.Danish, "da" },
            { Language.Divehi, "dv" },
            { Language.Dutch, "nl" },
            { Language.Dzongkha, "dz" },
            { Language.English, "en" },
            { Language.Esperanto, "eo" },
            { Language.Estonian, "et" },
            { Language.Ewe, "ee" },
            { Language.Faroese, "fo" },
            { Language.Fijian, "fj" },
            { Language.Finnish, "fi" },
            { Language.French, "fr" },
            { Language.WesternFrisian, "fy" },
            { Language.Fulah, "ff" },
            { Language.Gaelic, "gd" },
            { Language.Galician, "gl" },
            { Language.Ganda, "lg" },
            { Language.Georgian, "ka" },
            { Language.German, "de" },
            { Language.Greek, "el" },
            { Language.Kalaallisut, "kl" },
            { Language.Guarani, "gn" },
            { Language.Gujarati, "gu" },
            { Language.Haitian, "ht" },
            { Language.Hausa, "ha" },
            { Language.Hebrew, "he" },
            { Language.Herero, "hz" },
            { Language.Hindi, "hi" },
            { Language.HiriMotu, "ho" },
            { Language.Hungarian, "hu" },
            { Language.Icelandic, "is" },
            { Language.Ido, "io" },
            { Language.Igbo, "ig" },
            { Language.Indonesian, "id" },
            { Language.InterlinguaInternationalAuxiliaryLanguageAssociation, "ia" },
            { Language.Interlingue, "ie" },
            { Language.Inuktitut, "iu" },
            { Language.Inupiaq, "ik" },
            { Language.Irish, "ga" },
            { Language.Italian, "it" },
            { Language.Japanese, "ja" },
            { Language.Javanese, "jv" },
            { Language.Kannada, "kn" },
            { Language.Kanuri, "kr" },
            { Language.Kashmiri, "ks" },
            { Language.Kazakh, "kk" },
            { Language.CentralKhmer, "km" },
            { Language.Kikuyu, "ki" },
            { Language.Kinyarwanda, "rw" },
            { Language.Kirghiz, "ky" },
            { Language.Komi, "kv" },
            { Language.Kongo, "kg" },
            { Language.Korean, "ko" },
            { Language.Kuanyama, "kj" },
            { Language.Kurdish, "ku" },
            { Language.Lao, "lo" },
            { Language.Latin, "la" },
            { Language.Latvian, "lv" },
            { Language.Limburgan, "li" },
            { Language.Lingala, "ln" },
            { Language.Lithuanian, "lt" },
            { Language.LubaKatanga, "lu" },
            { Language.Luxembourgish, "lb" },
            { Language.Macedonian, "mk" },
            { Language.Malagasy, "mg" },
            { Language.Malay, "ms" },
            { Language.Malayalam, "ml" },
            { Language.Maltese, "mt" },
            { Language.Manx, "gv" },
            { Language.Maori, "mi" },
            { Language.Marathi, "mr" },
            { Language.Marshallese, "mh" },
            { Language.Mongolian, "mn" },
            { Language.Nauru, "na" },
            { Language.Navajo, "nv" },
            { Language.NorthNdebele, "nd" },
            { Language.SouthNdebele, "nr" },
            { Language.Ndonga, "ng" },
            { Language.Nepali, "ne" },
            { Language.Norwegian, "no" },
            { Language.NorwegianBokmål, "nb" },
            { Language.NorwegianNynorsk, "nn" },
            { Language.SichuanYi, "ii" },
            { Language.Occitan, "oc" },
            { Language.Ojibwa, "oj" },
            { Language.Oriya, "or" },
            { Language.Oromo, "om" },
            { Language.Ossetian, "os" },
            { Language.Pali, "pi" },
            { Language.Pashto, "ps" },
            { Language.Persian, "fa" },
            { Language.Polish, "pl" },
            { Language.Portuguese, "pt" },
            { Language.Punjabi, "pa" },
            { Language.Quechua, "qu" },
            { Language.Romanian, "ro" },
            { Language.Romansh, "rm" },
            { Language.Rundi, "rn" },
            { Language.Russian, "ru" },
            { Language.NorthernSami, "se" },
            { Language.Samoan, "sm" },
            { Language.Sango, "sg" },
            { Language.Sanskrit, "sa" },
            { Language.Sardinian, "sc" },
            { Language.Serbian, "sr" },
            { Language.Shona, "sn" },
            { Language.Sindhi, "sd" },
            { Language.Sinhala, "si" },
            { Language.Slovak, "sk" },
            { Language.Slovenian, "sl" },
            { Language.Somali, "so" },
            { Language.SouthernSotho, "st" },
            { Language.Spanish, "es" },
            { Language.Sundanese, "su" },
            { Language.Swahili, "sw" },
            { Language.Swati, "ss" },
            { Language.Swedish, "sv" },
            { Language.Tagalog, "tl" },
            { Language.Tahitian, "ty" },
            { Language.Tajik, "tg" },
            { Language.Tamil, "ta" },
            { Language.Tatar, "tt" },
            { Language.Telugu, "te" },
            { Language.Thai, "th" },
            { Language.Tibetan, "bo" },
            { Language.Tigrinya, "ti" },
            { Language.TongaTongaIslands, "to" },
            { Language.Tsonga, "ts" },
            { Language.Tswana, "tn" },
            { Language.Turkish, "tr" },
            { Language.Turkmen, "tk" },
            { Language.Twi, "tw" },
            { Language.Uighur, "ug" },
            { Language.Ukrainian, "uk" },
            { Language.Urdu, "ur" },
            { Language.Uzbek, "uz" },
            { Language.Venda, "ve" },
            { Language.Vietnamese, "vi" },
            { Language.Volapük, "vo" },
            { Language.Walloon, "wa" },
            { Language.Welsh, "cy" },
            { Language.Wolof, "wo" },
            { Language.Xhosa, "xh" },
            { Language.Yiddish, "yi" },
            { Language.Yoruba, "yo" },
            { Language.Zhuang, "za" },
            { Language.Zulu, "zu" },
            { Language.ChineseSimplified, "zh_simplified" },
            { Language.Unknown, "unknown" },
        };
    }
}
