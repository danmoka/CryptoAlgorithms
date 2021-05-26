using System;
using System.Collections.Generic;

namespace CryptoAlgorithms.Core.HastadAttack
{
    public class HastadAttackAlg
    {
        public IList<long> ChipherTexts { get; set; }
        public IList<long> Modules { get; set; }
        public int ChipherTextsLength { get => ChipherTexts.Count; }
        public int ModulesLength { get => Modules.Count; }

        /// <summary>
        /// Находит сообщение
        /// </summary>
        /// <returns>Сообщение</returns>
        public long GetMessage()
        {
            if (ChipherTextsLength < 1 ||
                ModulesLength < 1 ||
                ChipherTextsLength != ModulesLength)
            {
                throw new Exception($"Errors when getting the message:" +
                    $" chipher texts count {ChipherTextsLength}" +
                    $" modules count {ModulesLength}");
            }

            var items = new List<Tuple<long, long>>();

            for (int i = 0; i < ChipherTextsLength; i++)
            {
                items.Add(new Tuple<long, long>(ChipherTexts[i], Modules[i]));
            }

            var chipherText = ChineseRemainderTheorem.Solve(items);

            return Convert.ToInt64(Math.Exp(Math.Log(chipherText) / ChipherTextsLength));
        }
    }
}
