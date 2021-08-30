namespace Optima.Base
{
    public abstract class Solution
    {
        public virtual bool IsFeasible { get; set; }

        public virtual bool ValueWasCalculated { get; set; }
        public virtual bool PenaltyWasCalculated { get; set; }
        public virtual double CalculatedValue { get; set; }
        public virtual double CalculatedPenalty { get; set; }

        protected abstract void CopyTo(Solution solution);
        protected abstract void ResetToInitialState();

        public abstract Solution Clone();

        public void Copy(Solution copyTo)
        {
            copyTo.CalculatedPenalty = CalculatedPenalty;
            copyTo.CalculatedValue = CalculatedValue;
            copyTo.PenaltyWasCalculated = PenaltyWasCalculated;
            copyTo.ValueWasCalculated = ValueWasCalculated;
            copyTo.IsFeasible = IsFeasible;
            CopyTo(copyTo);
        }

        public void Reset()
        {
            ValueWasCalculated = false;
            PenaltyWasCalculated = false;
            IsFeasible = false;
            CalculatedValue = 0;
            CalculatedPenalty = 0;
            ResetToInitialState();
        }
    }
}