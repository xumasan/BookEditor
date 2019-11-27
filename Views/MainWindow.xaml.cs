using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Markup.Xaml;
using Avalonia.Layout;
using System;

using BookEditor;
using BookEditor.ViewModels;
using BookEditor.Models;

namespace BookEditor.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // pos.Set("lnsgkgsnl/1r5b1/ppppppppp/9/9/9/PPPPPPPPP/1B5R1/LNSGKGSNL b - 1");
            pos.Set("l6nl/5+P1gk/2np1S3/p1p4Pp/3P2Sp1/1PPb2P1P/P5GS1/R8/LN4bKL w RGgsn5p 1");
            DrawBoard();
            DrawHand();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DrawBoard()
        {
            for (var sq = Square.SQ_9A; sq <= Square.SQ_1I; ++sq)
            {
                var pc = pos.PieceOn(sq);
                if (pc == Piece.NO_PIECE) continue;
                DrawBoardPiece(pc, sq);
            }
        }

        private void DrawBoardPiece(Piece pc, Square sq)
        {
            File f = Files.FileIndex[(int)sq];
            Rank r = Ranks.RankIndex[(int)sq];

            ImageBrush img = new ImageBrush(){
                Source = new Bitmap(PieceImagePaths[(int)pc]),
            };
            
            Rectangle ra = new Rectangle() {
                Height = 50,
                Width  = 50,
                Fill   = img,
            };
            
            Grid.SetColumn(ra, (int)f);
            Grid.SetRow(ra, (int)r);

            var board = this.FindControl<Grid>("board");
            if (rectSquares[(int)f, (int)r] != null)
                board.Children.Remove(rectSquares[(int)f, (int)r]);

            board.Children.Add(ra);
        }

        void DrawHand()
        {
            for (var c = Models.Color.BLACK; c < Models.Color.COLOR_NB; ++c)
                for (var pt = PieceType.PAWN; pt < PieceType.KING; ++pt)
                {
                    int num = pos.HandNum(c, pt);
                    if (num > 0)
                        DrawHandPiece(c, pt, num);
                }
        }

        private void DrawHandPiece(Models.Color c, PieceType pt, int num)
        {
            int rowIndex = (int)pt - 1; 
            int imgIndex = (int)pt;
            if (c == Models.Color.WHITE)
            {
                rowIndex = 6 - rowIndex;
                imgIndex += 16;
            }

            ImageBrush img = new ImageBrush(){
                Source = new Bitmap(PieceImagePaths[imgIndex]),
            };
            
            Rectangle ra = new Rectangle() {
                Height = 50,
                Width  = 50,
                Fill   = img,
            };
            
            Grid.SetColumn(ra, 0);
            Grid.SetRow(ra, rowIndex);

            var handName = c == Models.Color.BLACK ? "bHand" : "wHand";
            var hand = this.FindControl<Grid>(handName);
            if (rectHands[(int)c, (int)pt] != null)
                hand.Children.Remove(rectHands[(int)c, (int)pt]);
            
            hand.Children.Add(ra);

            TextBlock numTxt = new TextBlock() {
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 20.0,
                Text = num.ToString(),
            };

            Grid.SetColumn(numTxt, 1);
            Grid.SetRow(numTxt, rowIndex);

            hand.Children.Add(numTxt);
        }

        private Rectangle[,]  rectSquares = new Rectangle[9, 9];
        private Rectangle[,]  rectHands   = new Rectangle[2, 8];
        private Position pos = new Position();

        private readonly string[] PieceImagePaths = 
        {
            "Assets/UNREACHABLE_1.png",
            "Assets/b_Pawn.png"  ,
            "Assets/b_Lance.png" ,
            "Assets/b_Knight.png",
            "Assets/b_Silver.png",
            "Assets/b_Bishop.png",
            "Assets/b_Rook.png"  ,
            "Assets/b_Gold.png"  ,
            "Assets/b_King.png"  ,
            "Assets/b_ProPawn.png"  ,
            "Assets/b_ProLance.png" ,
            "Assets/b_ProKnight.png",
            "Assets/b_ProSilver.png",
            "Assets/b_Horse.png"    ,
            "Assets/b_Dragon.png"   ,
            "Assets/UNREACHABLE_2.png",
            "Assets/UNREACHABLE_3.png",
            "Assets/w_Pawn.png"  ,
            "Assets/w_Lance.png" ,
            "Assets/w_Knight.png",
            "Assets/w_Silver.png",
            "Assets/w_Bishop.png",
            "Assets/w_Rook.png"  ,
            "Assets/w_Gold.png"  ,
            "Assets/w_King.png"  ,
            "Assets/w_ProPawn.png"  ,
            "Assets/w_ProLance.png" ,
            "Assets/w_ProKnight.png",
            "Assets/w_ProSilver.png",
            "Assets/w_Horse.png"    ,
            "Assets/w_Dragon.png"   ,
        };
    }
}