using System;
using System.Collections.Generic;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.ScoreService
{
    public interface IScoreService
    {
        void AddScore(Score score);

        List<Score> GetTopScores();

        void ClearScores();
    }
}
