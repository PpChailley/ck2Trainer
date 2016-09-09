using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Ck2.Trainer;
using Ck2.Trainer.OldProcessors;

namespace Ck2Trainer
{
    internal class PlayerMoraleToOneFileProcessor : IFileProcessor
    {
        public string PlayerId;
        public string SubUnitTextBlock = null;

        public enum Position
        {
            OutOfBlock,
            Declared,
            BracketsOpened,
            BracketsJustClosed
        }


        public Position CurrentInterlinePosition = Position.OutOfBlock;


        public string Process(string line)
        {
            line = ReadPlayerId(line);
            line = SetArmyMorale(line, PlayerId, 1f);

            return line;
        }

        private string SetArmyMorale(string line, string playerId, float value)
        {
            Debug.WriteLine(line);

            TellWhetherInSubUnit(line);

            switch(this.CurrentInterlinePosition)
            {
                case    Position.OutOfBlock:
                    return line;

                case    Position.Declared:
                case    Position.BracketsOpened:
                    SubUnitTextBlock += line;
                    return string.Empty;


                case    Position.BracketsJustClosed:
                    CurrentInterlinePosition = Position.OutOfBlock;
                    SubUnitTextBlock += "\n" + line;
                    var toreturn =  ReplaceInMultiLineBlock();
                    SubUnitTextBlock = String.Empty;
                    return toreturn;


                default:
                    throw new Exception();
            }

        }

        private string ReplaceInMultiLineBlock()
        {
            if (Regex.IsMatch(SubUnitTextBlock, "owner=" + this.PlayerId))
            {
                return Regex.Replace(SubUnitTextBlock, "morale=.*", "morale=1.0");
            }

            return SubUnitTextBlock;
        }

        private void TellWhetherInSubUnit(string line)
        {
            switch (CurrentInterlinePosition)
            {
                case Position.OutOfBlock:
                    if (line.Contains("sub_unit="))
                        CurrentInterlinePosition = Position.Declared;
                    break;

                case Position.Declared:
                    if (line.Contains(@"{"))
                        CurrentInterlinePosition = Position.BracketsOpened;
                    else
                        CurrentInterlinePosition = Position.BracketsJustClosed;
                    break;

                case Position.BracketsOpened:
                    if (line.Contains(@"{"))
                        throw new Exception("Cannot process more nesting at " + line);
                    if (line.Contains(@"}"))
                        CurrentInterlinePosition = Position.BracketsJustClosed;
                    break;

                case Position.BracketsJustClosed:
                    throw new Exception();

                default:
                    throw new Exception();
            }



        }


        private string ReadPlayerId(string line)
        {
            if (PlayerId == null)
                return line;


            string criteria = "id=";

            if (line.Contains(criteria))
            {
                PlayerId = line.Substring(line.IndexOf(criteria) + criteria.Length);
                FrmMain.AddLogEntry(String.Format("Character Id = '{0}'", PlayerId));
            }

            return line;
        }
    }
}