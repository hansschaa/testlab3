using P2DEngine.Engine;
using System.Drawing;

namespace P2DEngine
{
    public class Arkanoid : P2DGame // Le recomiendo copiar y pegar el código de su LAB 2 aquí (y obviamente, adaptarlo para que funcione con las nuevas implementaciones hechas en clase)
    {

        float mouseX;
        int dxX, dyY;

        Block ball;
        Block player;
        Image bg;

        public Arkanoid(int width, int height, int targetFPS) : base(width, height, targetFPS)
        {

            bg = P2DImageManager.Get("Background");

            player = new Block(width / 2, height - 100, 100, 20, P2DImageManager.Get("Player"));
            ball = new Block(width / 2, height / 2, 20, 20, P2DImageManager.Get("Blue"));

            dxX = 6;
            dyY = 6;

            P2DAudioManager.Play("background");

            //Claudio
            //P2DAudioManager.LoopAudio("background", ".mp3"); // Tocamos el sonido.
        }

        protected override void ProcessInput()
        {
            mouseX = P2DMouseManager.mouseLocation.X;

            player.X = (int)mouseX;
        }

        protected override void RenderGame(Graphics g)
        {
            // Dibujamos el fondo.
            g.DrawImage(bg, 0, 0, mainWindow.Width, mainWindow.Height);

            // Dibujamos el player
            player.Draw(g);

            //Dibujamos la pelota
            ball.Draw(g);
        }

        protected override void UpdateGame()
        {
            ball.X += dxX;
            ball.Y += dyY;

            //Paddle collision
            if (ball.X > player.X && ball.X <= player.X + 100 && ball.Y+20 > player.Y) {
                dyY *= -1;
                P2DAudioManager.Play("bounce"); // Tocamos el sonido.
            }

            //Wall collision
            if (ball.X + 20 > windowWidth || ball.X < 0) {
                
                dxX *= -1;
                P2DAudioManager.Play("bounce"); // Tocamos el sonido.
            }


            if (ball.Y + 20 > windowHeight || ball.Y < 0) {
                
                dyY *= -1;
                P2DAudioManager.Play("bounce"); // Tocamos el sonido.
            } 
        }
    }
}
