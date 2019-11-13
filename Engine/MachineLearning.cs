using System.Windows.Forms;
using TicTacToeML.Model;

namespace Engine
{
    public class Prediction
    {
        public string DataToPredict { get; set; }

        public string MakePredictionOfWinner(Tile[] tiles)
        {
            var input = new ModelInput();
            input.Tile1 = tiles[0].Value.ToString();
            input.Tile2 = tiles[1].Value.ToString();
            input.Tile3 = tiles[2].Value.ToString();
            input.Tile4 = tiles[3].Value.ToString();
            input.Tile5 = tiles[4].Value.ToString();
            input.Tile6 = tiles[5].Value.ToString();
            input.Tile7 = tiles[6].Value.ToString();
            input.Tile8 = tiles[7].Value.ToString();
            input.Tile9 = tiles[8].Value.ToString();

            ModelOutput output = ConsumeModel.Predict(input);
            return output.Prediction;
            /*
            PredictWinner pr = new PredictWinner();
            string prediction = pr.Predict(data);
            return prediction;
            */
        }
    }
}