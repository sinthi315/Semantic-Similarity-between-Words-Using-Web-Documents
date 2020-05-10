using HtmlAgilityPack;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Takale_Nandgaonkar
{
    public static class HtmlRemoval
    {
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++ )
            {
                char let = source[i];
                if(let == '<')
                {
                    inside = true;
                    continue;
                }
                if(let == '>')
                {
                    inside = false;
                    continue;
                }
                if(!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
        }
    }

    static class StopwordTool
    {
        static Dictionary<string, bool> _stops = new Dictionary<string, bool>
        {
            {"a", true},
            {"about", true},
            { "above", true },
	        { "across", true },
	        { "after", true },
	        { "afterwards", true },
	        { "again", true },
	        { "against", true },
	        { "all", true },
            { "almost", true },
	        { "alone", true },
	        { "along", true },
	        { "already", true },
	        { "also", true },
	        { "although", true },
	        { "always", true },
	        { "am", true },
	        { "among", true },
	        { "amongst", true },
	        { "amount", true },
	        { "an", true },
	        { "and", true },
	        { "another", true },
	        { "any", true },
	        { "anyhow", true },
	        { "anyone", true },
	        { "anything", true },
	        { "anyway", true },
	        { "anywhere", true },
	        { "are", true },
	        { "around", true },
	        { "as", true },
	        { "at", true },
	        { "back", true },
	        { "be", true },
	        { "became", true },
	        { "because", true },
	        { "become", true },
	        { "becomes", true },
	        { "becoming", true },
	        { "been", true },
	        { "before", true },
	        { "beforehand", true },
	        { "behind", true },
	        { "being", true },
	        { "below", true },
	        { "beside", true },
	        { "besides", true },
	        { "between", true },
	        { "beyond", true },
	        { "bill", true },
	        { "both", true },
	        { "bottom", true },
	        { "but", true },
	        { "by", true },
	        { "call", true },
	        { "can", true },
	        { "cannot", true },
	        { "cant", true },
	        { "co", true },
	        { "computer", true },
	        { "con", true },
	        { "could", true },
	        { "couldnt", true },
	        { "cry", true },
	        { "de", true },
	        { "describe", true },
	        { "detail", true },
	        { "do", true },
	        { "done", true },
	        { "down", true },
	        { "due", true },
	        { "during", true },
	        { "each", true },
	        { "eg", true },
	        { "eight", true },
	        { "either", true },
	        { "eleven", true },
	        { "else", true },
	        { "elsewhere", true },
	        { "empty", true },
	        { "enough", true },
	        { "etc", true },
	        { "even", true },
	        { "ever", true },
	        { "every", true },
	        { "everyone", true },
	        { "everything", true },
	        { "everywhere", true },
	        { "except", true },
	        { "few", true },
	        { "fifteen", true },
	        { "fify", true },
	        { "fill", true },
	        { "find", true },
	        { "fire", true },
	        { "first", true },
	        { "five", true },
	        { "for", true },
	        { "former", true },
	        { "formerly", true },
	        { "forty", true },
	        { "found", true },
	        { "four", true },
	        { "front", true },
	        { "full", true },
	        { "further", true },
	        { "get", true },
	        { "give", true },
	        { "go", true },
	        { "had", true },
	        { "has", true },
	        { "have", true },
	        { "he", true },
	        { "hence", true },
	        { "her", true },
	        { "here", true },
	        { "hereafter", true },
	        { "hereby", true },
	        { "herein", true },
	        { "hereupon", true },
	        { "hers", true },
	        { "herself", true },
	        { "him", true },
	        { "himself", true },
	        { "his", true },
	        { "how", true },
	        { "however", true },
	        { "hundred", true },
	        { "i", true },
	        { "ie", true },
	        { "if", true },
	        { "in", true },
	        { "inc", true },
	        { "indeed", true },
	        { "interest", true },
	        { "into", true },
	        { "is", true },
	        { "it", true },
	        { "its", true },
	        { "itself", true },
	        { "keep", true },
	        { "last", true },
	        { "latter", true },
	        { "latterly", true },
	        { "least", true },
	        { "less", true },
	        { "ltd", true },
	        { "made", true },
	        { "many", true },
	        { "may", true },
	        { "me", true },
	        { "meanwhile", true },
	        { "might", true },
	        { "mill", true },
	        { "mine", true },
	        { "more", true },
	        { "moreover", true },
	        { "most", true },
	        { "mostly", true },
	        { "move", true },
	        { "much", true },
	        { "must", true },
	        { "my", true },
	        { "myself", true },
	        { "name", true },
	        { "namely", true },
	        { "neither", true },
	        { "never", true },
	        { "nevertheless", true },
	        { "next", true },
	        { "nine", true },
	        { "no", true },
	        { "nobody", true },
	        { "none", true },
	        { "nor", true },
	        { "not", true },
	        { "nothing", true },
	        { "now", true },
	        { "nowhere", true },
	        { "of", true },
	        { "off", true },
	        { "often", true },
	        { "on", true },
	        { "once", true },
	        { "one", true },
	        { "only", true },
	        { "onto", true },
	        { "or", true },
	        { "other", true },
	        { "others", true },
	        { "otherwise", true },
	        { "our", true },
	        { "ours", true },
	        { "ourselves", true },
	        { "out", true },
	        { "over", true },
	        { "own", true },
	        { "part", true },
	        { "per", true },
	        { "perhaps", true },
	        { "please", true },
	        { "put", true },
	        { "rather", true },
	        { "re", true },
	        { "same", true },
	        { "see", true },
	        { "seem", true },
	        { "seemed", true },
	        { "seeming", true },
	        { "seems", true },
	        { "serious", true },
	        { "several", true },
	        { "she", true },
	        { "should", true },
	        { "show", true },
	        { "side", true },
	        { "since", true },
	        { "sincere", true },
	        { "six", true },
	        { "sixty", true },
	        { "so", true },
	        { "some", true },
	        { "somehow", true },
	        { "someone", true },
	        { "something", true },
	        { "sometime", true },
	        { "sometimes", true },
	        { "somewhere", true },
	        { "still", true },
	        { "such", true },
	        { "system", true },
	        { "take", true },
	        { "ten", true },
	        { "than", true },
	        { "that", true },
	        { "the", true },
	        { "their", true },
	        { "them", true },
	        { "themselves", true },
	        { "then", true },
	        { "thence", true },
	        { "there", true },
	        { "thereafter", true },
	        { "thereby", true },
	        { "therefore", true },
	        { "therein", true },
	        { "thereupon", true },
	        { "these", true },
	        { "they", true },
	        { "thick", true },
	        { "thin", true },
	        { "third", true },
	        { "this", true },
	        { "those", true },
	        { "though", true },
	        { "three", true },
	        { "through", true },
	        { "throughout", true },
	        { "thru", true },
	        { "thus", true },
	        { "to", true },
	        { "together", true },
	        { "too", true },
	        { "top", true },
	        { "toward", true },
	        { "towards", true },
	        { "twelve", true },
	        { "twenty", true },
	        { "two", true },
	        { "un", true },
	        { "under", true },
	        { "until", true },
	        { "up", true },
	        { "upon", true },
	        { "us", true },
	        { "very", true },
	        { "via", true },
	        { "was", true },
	        { "we", true },
	        { "well", true },
	        { "were", true },
	        { "what", true },
	        { "whatever", true },
	        { "when", true },
	        { "whence", true },
	        { "whenever", true },
	        { "where", true },
	        { "whereafter", true },
	        { "whereas", true },
	        { "whereby", true },
	        { "wherein", true },
	        { "whereupon", true },
	        { "wherever", true },
	        { "whether", true },
	        { "which", true },
	        { "while", true },
	        { "whither", true },
	        { "who", true },
	        { "whoever", true },
	        { "whole", true },
	        { "whom", true },
	        { "whose", true },
	        { "why", true },
	        { "will", true },
	        { "with", true },
	        { "within", true },
	        { "without", true },
	        { "would", true },
	        { "yet", true },
	        { "you", true },
	        { "your", true },
	        { "yours", true },
	        { "yourself", true },
	        { "yourselves", true }
        };

        static char[] _delimiters = new char[]
        {
            ' ',
            ',',
            ';',
            '.'
        };

        public static string RemoveStopwords(string input)
        {
            var words = input.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
            var found = new Dictionary<string, bool>();

            StringBuilder builder = new StringBuilder();

            foreach(string currentWord in words)
            {
                string lowerWord = currentWord.ToLower();
                if(!_stops.ContainsKey(lowerWord) && !found.ContainsKey(lowerWord))
                {
                    builder.Append(currentWord).Append(' ');
                    found.Add(lowerWord, true);
                }
            }

            return builder.ToString().Trim();
        }
    }

    static class FrenchstopwordTool
    {
        static Dictionary<string, bool> _stops = new Dictionary<string, bool>
        {
            { "a", true },
            { "à", true },
            { "â", true },
            { "abord", true },
            { "afin", true },
            { "ah", true },
            { "ai", true },
            { "as", true },
            { "aie", true },
            { "aies", true },
            { "aient", true },
            { "ait", true },
            { "ainsi", true },
            { "allaient", true },
            { "allo", true },
            { "allô", true },
            { "allons", true },
            { "alors", true },
            { "après", true },
            { "assez", true },
            { "attendu", true },
            { "au", true },
            { "aucun", true },
            { "aucuns", true },
            { "aucune", true },
            { "aujourd", true },
            { "aujourd'hui", true },
            { "auquel", true },
            { "aura", true },
            { "aurai", true },
            { "auras", true },
            { "aurons", true },
            { "aurez", true },
            { "auront", true },
            { "aurais", true },
            { "aurait", true },
            { "aurions", true },
            { "auriez", true },
            { "auraient", true },
            { "aussi", true },
            { "autre", true },
            { "autres", true },
            { "aux", true },
            { "auxquelles", true },
            { "auxquels", true },
            { "avaient", true },
            { "avais", true },
            { "avait", true },
            { "avant", true },
            { "avec", true },
            { "avoir", true },
            { "avour", true },
            { "avons", true },
            { "avez", true },
            { "avions", true },
            { "aviez", true },
            { "ayant", true },
            { "ayons", true },
            { "ayez", true },
            { "b", true },
            { "bah", true },
            { "beaucoup", true },
            { "bien", true },
            { "bigre", true },
            { "boum", true },
            { "bravo", true },
            { "brrr", true },
            { "bon", true },
            { "c", true },
            { "ça", true },
            { "car", true },
            { "ce", true },
            { "ceci", true },
            { "cela", true },
            { "celà", true },
            { "celle", true },
            { "celle-ci", true },
            { "celle-là", true },
            { "celles", true },
            { "celles-ci", true },
            { "celles-là", true },
            { "celui", true },
            { "celui-ci", true },
            { "celui-là", true },
            { "cent", true },
            { "cependant", true },
            { "certain", true },
            { "certaine", true },
            { "certaines", true },
            { "certains", true },
            { "certes", true },
            { "ces", true },
            { "cet", true },
            { "cette", true },
            { "ceux", true },
            { "ceux-ci", true },
            { "ceux-là", true },
            { "chacun", true },
            { "chaque", true },
            { "cher", true },
            { "chère", true },
            { "chères", true },
            { "chers", true },
            { "chez", true },
            { "chiche", true },
            { "chut", true },
            { "ci", true },
            { "cinq", true },
            { "cinquantaine", true },
            { "cinquante", true },
            { "cinquantième", true },
            { "cinquième", true },
            { "clac", true },
            { "clic", true },
            { "combien", true },
            { "comme", true },
            { "comment", true },
            { "compris", true },
            { "concernant", true },
            { "contre", true },
            { "couic", true },
            { "crac", true },
            { "d", true },
            { "da", true },
            { "dans", true },
            { "de", true },
            { "debout", true },
            { "dedans", true },
            { "dehors", true },
            { "delà", true },
            { "depuis", true },
            { "derrière", true },
            { "des", true },
            { "dès", true },
            { "désormais", true },
            { "début", true },
            { "desquelles", true },
            { "desquels", true },
            { "dessous", true },
            { "dessus", true },
            { "deux", true },
            { "deuxième", true },
            { "deuxièmement", true },
            { "devant", true },
            { "devers", true },
            { "devra", true },
            { "devrait", true },
            { "différent", true },
            { "différente", true },
            { "différentes", true },
            { "différents", true },
            { "dire", true },
            { "divers", true },
            { "diverse", true },
            { "diverses", true },
            { "dix", true },
            { "dix-huit", true },
            { "dixième", true },
            { "dix-neuf", true },
            { "dix-sept", true },
            { "doit", true },
            { "doivent", true },
            { "donc", true },
            { "dont", true },
            { "dos", true },
            { "douze", true },
            { "douzième", true },
            { "dring", true },
            { "du", true },
            { "duquel", true },
            { "durant", true },
            { "e", true },
            { "effet", true },
            { "eh", true },
            { "elle", true },
            { "elle-même", true },
            { "elles", true },
            { "elles-mêmes", true },
            { "en", true },
            { "encore", true },
            { "entre", true },
            { "envers", true },
            { "environ", true },
            { "es", true },
            { "ès", true },
            { "est", true },
            { "essai", true },
            { "et", true },
            { "etant", true },
            { "étaient", true },
            { "étais", true },
            { "état", true },
            { "était", true },
            { "étant", true },
            { "étions", true },
            { "étiez", true },
            { "etc", true },
            { "été", true },
            { "étée", true },
            { "étées", true },
            { "étés", true },
            { "etre", true },
            { "être", true },
            { "êtes", true },
            { "eu", true },
            { "eue", true },
            { "eues", true },
            { "eus", true },
            { "euh", true },
            { "eusse", true },
            { "eusses", true },
            { "eussions", true },
            { "eussiez", true },
            { "eussent", true },
            { "eut", true },
            { "eux", true },
            { "eût", true },
            { "eûmes", true },
            { "eûtes", true },
            { "eurent", true },
            { "eux-mêmes", true },
            { "excepté", true },
            { "f", true },
            { "façon", true },
            { "fais", true },
            { "faisaient", true },
            { "faisant", true },
            { "fait", true },
            { "faites", true },
            { "feront", true },
            { "fi", true },
            { "flac", true },
            { "floc", true },
            { "font", true },
            { "fois", true },
            { "fus", true },
            { "fut", true },
            { "fûmes", true },
            { "fûtes", true },
            { "fût", true },
            { "furent", true },
            { "fusse", true },
            { "fusses", true },
            { "fussions", true },
            { "fussiez", true },
            { "fussent", true },
            { "g", true },
            { "gens", true },
            { "h", true },
            { "ha", true },
            { "hé", true },
            { "hein", true },
            { "hélas", true },
            { "hem", true },
            { "hep", true },
            { "hi", true },
            { "ho", true },
            { "holà", true },
            { "hop", true },
            { "hormis", true },
            { "hors", true },
            { "hou", true },
            { "houp", true },
            { "hue", true },
            { "hui", true },
            { "huit", true },
            { "huitième", true },
            { "hum", true },
            { "hurrah", true },
            { "i", true },
            { "il", true },
            { "ils", true },
            { "ici", true },
            { "importe", true },
            { "j", true },
            { "je", true },
            { "juste", true },
            { "jusqu", true },
            { "jusque", true },
            { "k", true },
            { "l", true },
            { "la", true },
            { "là", true },
            { "laquelle", true },
            { "las", true },
            { "le", true },
            { "lequel", true },
            { "les", true },
            { "lès", true },
            { "lesquelles", true },
            { "lesquels", true },
            { "leur", true },
            { "leurs", true },
            { "longtemps", true },
            { "lorsque", true },
            { "lui", true },
            { "lui-même", true },
            { "m", true },
            { "ma", true },
            { "maint", true },
            { "maintenant", true },
            { "mais", true },
            { "malgré", true },
            { "me", true },
            { "même", true },
            { "mêmes", true },
            { "merci", true },
            { "mes", true },
            { "mine", true },
            { "mien", true },
            { "mienne", true },
            { "miennes", true },
            { "miens", true },
            { "mille", true },
            { "mince", true },
            { "moi", true },
            { "moi-même", true },
            { "moins", true },
            { "mon", true },
            { "mot", true },
            { "moyennant", true },
            { "n", true },
            { "na", true },
            { "ne", true },
            { "néanmoins", true },
            { "neuf", true },
            { "neuvième", true },
            { "ni", true },
            { "nommés", true },
            { "nombreuses", true },
            { "nombreux", true },
            { "non", true },
            { "nos", true },
            { "notre", true },
            { "nôtre", true },
            { "nôtres", true },
            { "nous", true },
            { "nous-mêmes", true },
            { "nul", true },
            { "o", true },
            { "o|", true },
            { "ô", true },
            { "oh", true },
            { "ohé", true },
            { "olé", true },
            { "ollé", true },
            { "on", true },
            { "ont", true },
            { "onze", true },
            { "onzième", true },
            { "ore", true },
            { "ou", true },
            { "où", true },
            { "ouf", true },
            { "ouias", true },
            { "oust", true },
            { "ouste", true },
            { "outre", true },
            { "p", true },
            { "paf", true },
            { "pan", true },
            { "par", true },
            { "parmi", true },
            { "parce", true },
            { "partant", true },
            { "particulier", true },
            { "particulière", true },
            { "particulièrement", true },
            { "pas", true },
            { "passé", true },
            { "pendant", true },
            { "personne", true },
            { "peu", true },
            { "peut", true },
            { "peuvent", true },
            { "peux", true },
            { "pff", true },
            { "pfft", true },
            { "pfut", true },
            { "pif", true },
            { "plein", true },
            { "plouf", true },
            { "plus", true },
            { "plupart", true },
            { "plusieurs", true },
            { "plutôt", true },
            { "pouah", true },
            { "pour", true },
            { "pourquoi", true },
            { "premier", true },
            { "première", true },
            { "premièrement", true },
            { "près", true },
            { "proche", true },
            { "psitt", true },
            { "puisque", true },
            { "q", true },
            { "qu", true },
            { "quand", true },
            { "quant", true },
            { "quanta", true },
            { "quant-à-soi", true },
            { "quarante", true },
            { "quatorze", true },
            { "quatre", true },
            { "quatre-vingt", true },
            { "quatrième", true },
            { "quatrièmement", true },
            { "que", true },
            { "quel", true },
            { "quelconque", true },
            { "quelle", true },
            { "quelles", true },
            { "quelque", true },
            { "quelques", true },
            { "quelqu'un", true },
            { "quels", true },
            { "qui", true },
            { "quiconque", true },
            { "quinze", true },
            { "quoi", true },
            { "quoique", true },
            { "r", true },
            { "revoici", true },
            { "revoilà", true },
            { "rien", true },
            { "s", true },
            { "sa", true },
            { "sacrebleu", true },
            { "sans", true },
            { "sapristi", true },
            { "sauf", true },
            { "se", true },
            { "seize", true },
            { "selon", true },
            { "sept", true },
            { "septième", true },
            { "sera", true },
            { "serai", true },
            { "seras", true },
            { "serons", true },
            { "serez", true },
            { "seront", true },
            { "serais", true },
            { "serait", true },
            { "serions", true },
            { "seriez", true },
            { "serient", true },
            { "ses", true },
            { "seulement", true },
            { "si", true },
            { "sien", true },
            { "sienne", true },
            { "siennes", true },
            { "siens", true },
            { "sinon", true },
            { "six", true },
            { "sixième", true },
            { "soi", true },
            { "soi-même", true },
            { "sois", true },
            { "soit", true },
            { "soixante", true },
            { "son", true },
            { "sont", true },
            { "sommes", true },
            { "sous", true },
            { "soyons", true },
            { "soyez", true },
            { "soient", true },
            { "stop", true },
            { "suis", true },
            { "suivant", true },
            { "sur", true },
            { "surtout", true },
            { "sujet", true },
            { "t", true },
            { "ta", true },
            { "tac", true },
            { "tant", true },
            { "te", true },
            { "té", true },
            { "tel", true },
            { "telle", true },
            { "tellement", true },
            { "telles", true },
            { "tels", true },
            { "tenant", true },
            { "tandis", true },
            { "tes", true },
            { "tic", true },
            { "tien", true },
            { "tienne", true },
            { "tiennes", true },
            { "tiens", true },
            { "toc", true },
            { "toi", true },
            { "toi-même", true },
            { "ton", true },
            { "touchant", true },
            { "toujours", true },
            { "tous", true },
            { "tout", true },
            { "toute", true },
            { "toutes", true },
            { "treize", true },
            { "trente", true },
            { "très", true },
            { "trois", true },
            { "troisième", true },
            { "troisièmement", true },
            { "trop", true },
            { "tsoin", true },
            { "tsouin", true },
            { "tu", true },
            { "u", true },
            { "un", true },
            { "une", true },
            { "unes", true },
            { "uns", true },
            { "v", true },
            { "va", true },
            { "vais", true },
            { "vas", true },
            { "vé", true },
            { "vers", true },
            { "via", true },
            { "vif", true },
            { "vifs", true },
            { "vingt", true },
            { "vivat", true },
            { "vive", true },
            { "vives", true },
            { "vlan", true },
            { "voici", true },
            { "voilà", true },
            { "voient", true },
            { "vont", true },
            { "vos", true },
            { "votre", true },
            { "vôtre", true },
            { "vôtres", true },
            { "vous", true },
            { "vous-mêmes", true },
            { "vu", true },
            { "w", true },
            { "x", true },
            { "y", true },
            { "z", true },
            { "zut", true },
        };

        static char[] _delimiters = new char[]
        {
            ' ',
            ',',
            ';',
            '.',
            '?',
            '-',
            '[',
            ']',
            '(',
            ')'
        };

        public static string RemoveStopwords(string input)
        {
            var words = input.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);

            var found = new Dictionary<string, bool>();

            StringBuilder builder = new StringBuilder();

            foreach (string currentWord in words)
            {
                string lowerWord = currentWord.ToLower();
                if (!_stops.ContainsKey(lowerWord) && !found.ContainsKey(lowerWord))
                {
                    builder.Append(currentWord).Append(' ');
                    found.Add(lowerWord, true);
                }
            }
            return builder.ToString().Trim();
        }
    }

    class Stemmer
    {
        private char[] b;
        private int i,     /* offset into b */
            i_end, /* offset to end of stemmed word */
            j, k;
        private static int INC = 50;
        /* unit of size whereby b is increased */

        public Stemmer()
        {
            b = new char[INC];
            i = 0;
            i_end = 0;
        }

        /**
         * Add a character to the word being stemmed.  When you are finished
         * adding characters, you can call stem(void) to stem the word.
         */

        public void add(char ch)
        {
            if (i == b.Length)
            {
                char[] new_b = new char[i + INC];
                for (int c = 0; c < i; c++)
                    new_b[c] = b[c];
                b = new_b;
            }
            b[i++] = ch;
        }


        /** Adds wLen characters to the word being stemmed contained in a portion
         * of a char[] array. This is like repeated calls of add(char ch), but
         * faster.
         */

        public void add(char[] w, int wLen)
        {
            if (i + wLen >= b.Length)
            {
                char[] new_b = new char[i + wLen + INC];
                for (int c = 0; c < i; c++)
                    new_b[c] = b[c];
                b = new_b;
            }
            for (int c = 0; c < wLen; c++)
                b[i++] = w[c];
        }

        /**
         * After a word has been stemmed, it can be retrieved by toString(),
         * or a reference to the internal buffer can be retrieved by getResultBuffer
         * and getResultLength (which is generally more efficient.)
         */

        public override string ToString()
        {
            return new String(b, 0, i_end);
        }

        /**
         * Returns the length of the word resulting from the stemming process.
         */
        public int getResultLength()
        {
            return i_end;
        }

        /**
         * Returns a reference to a character buffer containing the results of
         * the stemming process.  You also need to consult getResultLength()
         * to determine the length of the result.
         */
        public char[] getResultBuffer()
        {
            return b;
        }

        /* cons(i) is true <=> b[i] is a consonant. */
        private bool cons(int i)
        {
            switch (b[i])
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u': return false;
                case 'y': return (i == 0) ? true : !cons(i - 1);
                default: return true;
            }
        }

        /* m() measures the number of consonant sequences between 0 and j. if c is
           a consonant sequence and v a vowel sequence, and <..> indicates arbitrary
           presence,

              <c><v>       gives 0
              <c>vc<v>     gives 1
              <c>vcvc<v>   gives 2
              <c>vcvcvc<v> gives 3
              ....
        */
        private int m()
        {
            int n = 0;
            int i = 0;
            while (true)
            {
                if (i > j) return n;
                if (!cons(i)) break; i++;
            }
            i++;
            while (true)
            {
                while (true)
                {
                    if (i > j) return n;
                    if (cons(i)) break;
                    i++;
                }
                i++;
                n++;
                while (true)
                {
                    if (i > j) return n;
                    if (!cons(i)) break;
                    i++;
                }
                i++;
            }
        }

        /* vowelinstem() is true <=> 0,...j contains a vowel */
        private bool vowelinstem()
        {
            int i;
            for (i = 0; i <= j; i++)
                if (!cons(i))
                    return true;
            return false;
        }

        /* doublec(j) is true <=> j,(j-1) contain a double consonant. */
        private bool doublec(int j)
        {
            if (j < 1)
                return false;
            if (b[j] != b[j - 1])
                return false;
            return cons(j);
        }

        /* cvc(i) is true <=> i-2,i-1,i has the form consonant - vowel - consonant
           and also if the second c is not w,x or y. this is used when trying to
           restore an e at the end of a short word. e.g.

              cav(e), lov(e), hop(e), crim(e), but
              snow, box, tray.

        */
        private bool cvc(int i)
        {
            if (i < 2 || !cons(i) || cons(i - 1) || !cons(i - 2))
                return false;
            int ch = b[i];
            if (ch == 'w' || ch == 'x' || ch == 'y')
                return false;
            return true;
        }

        private bool ends(String s)
        {
            int l = s.Length;
            int o = k - l + 1;
            if (o < 0)
                return false;
            char[] sc = s.ToCharArray();
            for (int i = 0; i < l; i++)
                if (b[o + i] != sc[i])
                    return false;
            j = k - l;
            return true;
        }

        /* setto(s) sets (j+1),...k to the characters in the string s, readjusting
           k. */
        private void setto(String s)
        {
            int l = s.Length;
            int o = j + 1;
            char[] sc = s.ToCharArray();
            for (int i = 0; i < l; i++)
                b[o + i] = sc[i];
            k = j + l;
        }

        /* r(s) is used further down. */
        private void r(String s)
        {
            if (m() > 0)
                setto(s);
        }

        /* step1() gets rid of plurals and -ed or -ing. e.g.
               caresses  ->  caress
               ponies    ->  poni
               ties      ->  ti
               caress    ->  caress
               cats      ->  cat

               feed      ->  feed
               agreed    ->  agree
               disabled  ->  disable

               matting   ->  mat
               mating    ->  mate
               meeting   ->  meet
               milling   ->  mill
               messing   ->  mess

               meetings  ->  meet

        */

        private void step1()
        {
            if (b[k] == 's')
            {
                if (ends("sses"))
                    k -= 2;
                else if (ends("ies"))
                    setto("i");
                else if (b[k - 1] != 's')
                    k--;
            }
            if (ends("eed"))
            {
                if (m() > 0)
                    k--;
            }
            else if ((ends("ed") || ends("ing")) && vowelinstem())
            {
                k = j;
                if (ends("at"))
                    setto("ate");
                else if (ends("bl"))
                    setto("ble");
                else if (ends("iz"))
                    setto("ize");
                else if (doublec(k))
                {
                    k--;
                    int ch = b[k];
                    if (ch == 'l' || ch == 's' || ch == 'z')
                        k++;
                }
                else if (m() == 1 && cvc(k)) setto("e");
            }
        }

        /* step2() turns terminal y to i when there is another vowel in the stem. */
        private void step2()
        {
            if (ends("y") && vowelinstem())
                b[k] = 'i';
        }

        /* step3() maps double suffices to single ones. so -ization ( = -ize plus
           -ation) maps to -ize etc. note that the string before the suffix must give
           m() > 0. */
        private void step3()
        {
            if (k == 0)
                return;

            /* For Bug 1 */
            switch (b[k - 1])
            {
                case 'a':
                    if (ends("ational")) { r("ate"); break; }
                    if (ends("tional")) { r("tion"); break; }
                    break;
                case 'c':
                    if (ends("enci")) { r("ence"); break; }
                    if (ends("anci")) { r("ance"); break; }
                    break;
                case 'e':
                    if (ends("izer")) { r("ize"); break; }
                    break;
                case 'l':
                    if (ends("bli")) { r("ble"); break; }
                    if (ends("alli")) { r("al"); break; }
                    if (ends("entli")) { r("ent"); break; }
                    if (ends("eli")) { r("e"); break; }
                    if (ends("ousli")) { r("ous"); break; }
                    break;
                case 'o':
                    if (ends("ization")) { r("ize"); break; }
                    if (ends("ation")) { r("ate"); break; }
                    if (ends("ator")) { r("ate"); break; }
                    break;
                case 's':
                    if (ends("alism")) { r("al"); break; }
                    if (ends("iveness")) { r("ive"); break; }
                    if (ends("fulness")) { r("ful"); break; }
                    if (ends("ousness")) { r("ous"); break; }
                    break;
                case 't':
                    if (ends("aliti")) { r("al"); break; }
                    if (ends("iviti")) { r("ive"); break; }
                    if (ends("biliti")) { r("ble"); break; }
                    break;
                case 'g':
                    if (ends("logi")) { r("log"); break; }
                    break;
                default:
                    break;
            }
        }

        /* step4() deals with -ic-, -full, -ness etc. similar strategy to step3. */
        private void step4()
        {
            switch (b[k])
            {
                case 'e':
                    if (ends("icate")) { r("ic"); break; }
                    if (ends("ative")) { r(""); break; }
                    if (ends("alize")) { r("al"); break; }
                    break;
                case 'i':
                    if (ends("iciti")) { r("ic"); break; }
                    break;
                case 'l':
                    if (ends("ical")) { r("ic"); break; }
                    if (ends("ful")) { r(""); break; }
                    break;
                case 's':
                    if (ends("ness")) { r(""); break; }
                    break;
            }
        }

        /* step5() takes off -ant, -ence etc., in context <c>vcvc<v>. */
        private void step5()
        {
            if (k == 0)
                return;

            /* for Bug 1 */
            switch (b[k - 1])
            {
                case 'a':
                    if (ends("al")) break; return;
                case 'c':
                    if (ends("ance")) break;
                    if (ends("ence")) break; return;
                case 'e':
                    if (ends("er")) break; return;
                case 'i':
                    if (ends("ic")) break; return;
                case 'l':
                    if (ends("able")) break;
                    if (ends("ible")) break; return;
                case 'n':
                    if (ends("ant")) break;
                    if (ends("ement")) break;
                    if (ends("ment")) break;
                    /* element etc. not stripped before the m */
                    if (ends("ent")) break; return;
                case 'o':
                    if (ends("ion") && j >= 0 && (b[j] == 's' || b[j] == 't')) break;
                    /* j >= 0 fixes Bug 2 */
                    if (ends("ou")) break; return;
                /* takes care of -ous */
                case 's':
                    if (ends("ism")) break; return;
                case 't':
                    if (ends("ate")) break;
                    if (ends("iti")) break; return;
                case 'u':
                    if (ends("ous")) break; return;
                case 'v':
                    if (ends("ive")) break; return;
                case 'z':
                    if (ends("ize")) break; return;
                default:
                    return;
            }
            if (m() > 1)
                k = j;
        }

        /* step6() removes a final -e if m() > 1. */
        private void step6()
        {
            j = k;

            if (b[k] == 'e')
            {
                int a = m();
                if (a > 1 || a == 1 && !cvc(k - 1))
                    k--;
            }
            if (b[k] == 'l' && doublec(k) && m() > 1)
                k--;
        }

        /** Stem the word placed into the Stemmer buffer through calls to add().
         * Returns true if the stemming process resulted in a word different
         * from the input.  You can retrieve the result with
         * getResultLength()/getResultBuffer() or toString().
         */
        public void stem()
        {
            k = i - 1;
            if (k > 1)
            {
                step1();
                step2();
                step3();
                step4();
                step5();
                step6();
            }
            i_end = k + 1;
            i = 0;
        }

        /** Test program for demonstrating the Stemmer.  It reads text from a
         * a list of files, stems each word, and writes the result to standard
         * output. Note that the word stemmed is expected to be in lower case:
         * forcing lower case must be done outside the Stemmer class.
         * Usage: Stemmer file-name file-name ...
         */
        public static string[] Stemming(string text)
        {
            string[] args = text.Split();
            string[] Final = new string[args.Length];
            if(args.Length == 0)
            {
                Console.WriteLine("Usage:  Stemmer <input file>");
                return Final;
            }
            char[] w = new char[501];
            Stemmer s = new Stemmer();
            for (int i = 0; i < args.Length; i++)
            {
                byte[] array = Encoding.ASCII.GetBytes(args[i]);
                int Size = array.Length;
                int Count = 0;
                int j = 0;
                Array.Clear(w, 0, w.Length);
                
                foreach(byte element in array)
                {
                    Count++;
                    int ch = element;
                    if (Char.IsLetter((char)ch))
                    {
                        ch = Char.ToLower((char)ch);
                        w[j] = (char)ch;
                        if (j < 500)
                        {
                            j++;
                        }
                        if (Count == Size)
                        {
                            for (int c = 0; c < j; c++)
                            {
                                s.add(w[c]);
                            }
                            s.stem();
                            String u;
                            
                            u = s.ToString();
                            Final[i] = u;
                            break;
                        }
                    }
                    if (ch < 0)
                    {
                        break;
                    }
                    Final[i] = ch.ToString();
                }
            }
            return Final;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string line1;
            string line2;
            float jaccardCoefficient = 0;
            float diceCoefficient = 0;
            float overlapCoefficient = 0;
            float cosineSimilarityMeasure = 0;
            float simpleMatchingCoefficient = 0;
            List<string> snippet1 = new List<string>();
            List<string> snippet2 = new List<string>();
            List<string> firstWords = new List<string>();
            List<string> secondWords = new List<string>();
            List<string> filteredString1 = new List<string>();
            List<string> filteredString2 = new List<string>();
            string[] steaming1 = new string[filteredString1.Count];
            string[] steaming2 = new string[filteredString2.Count];


            FileStream file1 = File.Open("C:\\Users\\saima\\Documents\\Visual Studio 2015\\Projects\\Takale_Nandgaonkar\\test1.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BufferedStream bs1 = new BufferedStream(file1);
            StreamReader sr1 = new StreamReader(bs1);

            FileStream file2 = File.Open("C:\\Users\\saima\\Documents\\Visual Studio 2015\\Projects\\Takale_Nandgaonkar\\test2.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BufferedStream bs2 = new BufferedStream(file2);
            StreamReader sr2 = new StreamReader(bs2);

            while ((line1 = sr1.ReadLine()) != null)
            {
                char[] delimiterChars = { ' ', ',', '.', ':', '?', '!', ';' };
                string[] words = line1.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                /*Console.WriteLine("Filtered Line is: {0}", filteredLine1);*/
                foreach (string str in words)
                {
                    firstWords.Add(str);
                }
            }

            foreach (string firstWord in firstWords)
            {
                snippet1 = SnippetExtraction(firstWord);
            }

            file1.Close();

            /*Console.WriteLine("Enter first word: ");
            string firstWord = Console.ReadLine();

            Console.WriteLine("Enter second word: ");
            string secondWord = Console.ReadLine();*/

            while ((line2 = sr2.ReadLine()) != null)
            {
                char[] delimiterChars = { ' ', ',', '.', ':', '?', '!', ';' };
                string[] words = line2.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                /*Console.WriteLine("Filtered Line is: {0}", filteredLine1);*/
                foreach (string str in words)
                {
                    secondWords.Add(str) ;
                }
            }

            foreach(string secondWord in secondWords)
            {
                snippet2 = SnippetExtraction(secondWord);
            }

            file2.Close();
            
            foreach(string str in snippet1)
            {
                string input = StopwordTool.RemoveStopwords(str);
                filteredString1.Add(input);
            }

            foreach(string s in snippet2)
            {
                string input = StopwordTool.RemoveStopwords(s);
                filteredString2.Add(input);
            }
            
            
            foreach(string sr in filteredString1)
            {
                steaming1 = Stemmer.Stemming(sr);
            }

            foreach(string filter in filteredString2)
            {
                steaming2 = Stemmer.Stemming(filter);
            }
            
            var intersect = steaming1.Intersect(steaming2);
            var union = steaming1.Union(steaming2);
            
            jaccardCoefficient = (float)intersect.Count() / (steaming1.Length + steaming2.Length - intersect.Count());
            diceCoefficient = 2 * (float)intersect.Count() / (steaming1.Length + steaming2.Length);
            overlapCoefficient = (float)intersect.Count() / (float)Math.Min(steaming1.Length, steaming2.Length);
            cosineSimilarityMeasure = intersect.Count() / (float)(Math.Sqrt(steaming1.Length) * Math.Sqrt(steaming2.Length));
            simpleMatchingCoefficient = (float) intersect.Count() / union.Count();


            Console.WriteLine("\nJaccard Coefficient: {0}\n", jaccardCoefficient);
            Console.WriteLine("Dice Coefficient: {0}\n", diceCoefficient);
            Console.WriteLine("Overlap Coefficient: {0}\n", overlapCoefficient);
            Console.WriteLine("Cosine Similarity Measure: {0}\n", cosineSimilarityMeasure);
            Console.WriteLine("Simple Matching Coefficient: {0}\n", simpleMatchingCoefficient);
        }

        static List<string> SnippetExtraction(string word)
        {
            HtmlDocument doc = new HtmlDocument();
            List<string> snippet = new List<string>();
            WebClient web = new WebClient();

            //Process.Start("https://en.wikipedia.org/wiki/" + word);
            string snippetSet = web.DownloadString("https://en.wikipedia.org/wiki/" + word);
            string regex = "<p>(.*?)</p>";
            MatchCollection m = new Regex(regex, RegexOptions.Singleline | RegexOptions.Compiled).Matches(snippetSet);

            foreach (Match mtch in m)
            {
                string list = mtch.Groups[0].Value;
                snippet.Add(HtmlRemoval.StripTagsRegexCompiled(list));
            }

            return snippet;
        }
    }
}
