using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ck2.Mapping.Save.Extensions
{
    public static class StringExtension
    {

        public static IEnumerable<string> SplitAndKeep(this string s, char[] delims)
        {
            int start = 0, index;

            while ((index = s.IndexOfAny(delims, start)) != -1)
            {
                if (index - start > 0)
                    yield return s.Substring(start, index - start);
                yield return s.Substring(index, 1);
                start = index + 1;
            }

            if (start < s.Length)
            {
                yield return s.Substring(start);
            }
        }

        public static string RemoveAll(this string s, char[] willBeRemoved)
        {
            var sb = new StringBuilder(s.Length);

            foreach (var c in s.ToCharArray())
            {
                if (willBeRemoved.Contains(c) == false)
                    sb.Append(c);
            }

            return sb.ToString();
        }


        public static long EqualsWithIgnores(this string me, string you, char[] ignoredChars)
        {
            var a = me.ToCharArray();
            var b = you.ToCharArray();

            long indexA = 0L;
            long indexInB = 0L;
            var nbLinesSeenA = 1;
            var nbLinesSeenB = 1;
            int colA = 1;
            int colB = 1;

            bool stopOnBreaks = false;

            while (indexA < a.LongLength && indexInB < b.LongLength )
            {

                if (ignoredChars.Contains(a[indexA]))
                {
                    if (a[indexA] == '\n')
                    {
                        nbLinesSeenA++;
                        colA = 1;
                    }
                    else
                    {
                        colA += a[indexA] == '\t' ? 4 : 1;
                    }

                    indexA++;
                    continue;
                }

                if (ignoredChars.Contains(b[indexInB]))
                {
                    if (b[indexInB] == '\n')
                    {
                        nbLinesSeenB++;
                        colB = 1;
                    }
                    else
                    {
                        colB += b[indexInB] == '\t' ? 4 : 1;
                    }

                    indexInB++;
                    continue;
                }


                if (nbLinesSeenA >= 41 || nbLinesSeenB >= 41)
                    stopOnBreaks = false;

                if (a[indexA] != b[indexInB])
                {
                    var s = 0;
                    return nbLinesSeenA;
                }

                indexA ++;
                colA ++;

                indexInB ++;
                colB ++;
            }

            return 0;

        }



    }
}
