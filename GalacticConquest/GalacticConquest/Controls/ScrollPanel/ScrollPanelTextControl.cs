using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls.ScrollPanel
{
    public class ScrollPanelTextControl
    {
        public Texture2D scrollPanelImg;

        public Vector2 vectorPos;

        public Rectangle rectControl;

        //private string scrlPnlText;

        //public string ScrlPnlText
        //{
        //    get { return scrlPnlText; }
        //    set 
        //    { 
        //        scrlPnlText = value;
        //        arryPnlText = Model.Utilities.parseText(scrlPnlText,rectControl.Width,curs
        //    }
        //}
        public string scrlPnlText;


        //public string[] arryPnlText;


        public Controls.PanelControl innerPanel;


        public bool _isNull;
        public bool _isEnabled;


        public ScrollPanelTextControl()
        {
            Hide();

            vectorPos = Vector2.Zero;
        }


        public ScrollPanelTextControl(Texture2D newScrollPanelImg, Vector2 newVectorPos, Rectangle newRectControl)
        {
            scrollPanelImg = newScrollPanelImg;

            vectorPos = newVectorPos;
            rectControl = newRectControl;

            scrlPnlText = "";

            Show();
        }


        public void Hide()
        {
            _isNull = true;
            _isEnabled = false;
        }

        public void Show()
        {
            _isNull = false;
            _isEnabled = true;
        }




        //private void RedrawScroller()
        //{
        //    int sizeX = size.X;
        //    int sizeY = size.Y;

        //    switch (axis)
        //    {
        //        case Axis.Horizontal:
        //            sizeX = (int)(size.X / System.Math.Min(max, size.X / 20));
        //            sizeY -= 2;
        //            break;
        //        case Axis.Vertical:
        //            sizeY = (int)(size.Y / System.Math.Min(max, size.Y / 20));
        //            sizeX -= 2;
        //            break;
        //    }

        //    scrollerTex = new Texture2D(BaseGame.Device, sizeX, sizeY, 1, Control.resourceUsage, SurfaceFormat.Color);

        //    Color[] pixel = new Color[sizeX * sizeY];

        //    for (int y = 0; y < sizeY; y++)
        //    {
        //        for (int x = 0; x < sizeX; x++)
        //        {
        //            pixel[x + y * sizeX] = new Color(new Vector4(0.7f, 0.7f, 0.7f, 1f));

        //            float cX = pixel[x + y * sizeX].ToVector3().X;
        //            float cY = pixel[x + y * sizeX].ToVector3().X;
        //            float cZ = pixel[x + y * sizeX].ToVector3().X;

        //            if (x < 2 || y < 2)
        //            {
        //                cX *= 1.15f;
        //                cY *= 1.15f;
        //                cZ *= 1.15f;
        //            }
        //            else if (x > sizeX - 3 || y > sizeY - 3)
        //            {
        //                cX *= .75f;
        //                cY *= .75f;
        //                cZ *= .75f;
        //            }

        //            pixel[x + y * sizeX] = new Color(new Vector4(cX, cY, cZ, 1f));
        //        }
        //    }

        //    scrollerTex.SetData<Color>(pixel);
        //}



        //Draw Methods
        public void Draw(GameDrawClassComponents curGameDrawComponents)
        {
            
            
            //Texture2D visibleBuff = new Texture2D(curGameDrawComponents._graphicsDevice, rectControl.Width, (int)Model.Utilities.getTextHeight(scrlPnlText,curGameDrawComponents._staticFonts));


            //GraphicsDevice mgd = curGameDrawComponents._graphicsDevice;
            //mgd.Viewport.Height = rectControl.Height;


            
            //ScrollPanel
            curGameDrawComponents._spriteBatch.Draw(this.scrollPanelImg, this.rectControl, Color.White);

            //visibleBuff.
            //Effect scrlPnlFX = curGameDrawComponents._Content.Load<Effect>("scrollPanelFX");



            //spr
            //scrlPnlFX.Parameters["fShieldFacing"].SetValue(shieldFacing);
            //scrlPnlFX.Parameters["fShieldSize"].SetValue(shieldSize);
            //scrlPnlFX.Parameters["fShieldInnerRadius"].SetValue(shieldInnerRadius); 


            //Display Description
            Vector2 descVector = new Vector2(this.rectControl.X + 10, this.rectControl.Y + 10);
            curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, Model.Utilities.parseTextWidthHeight(this.scrlPnlText, this.rectControl.Width - 60, this.rectControl.Height - 20, curGameDrawComponents._staticFonts), descVector, Color.Black);

        }

        public static Texture2D Crop(Texture2D source, Rectangle area)  
        {  
            if (source == null)  
            return null;  

            Texture2D cropped = new Texture2D(source.GraphicsDevice, area.Width, area.Height);  
            Color[] data = new Color[source.Width * source.Height];  
            Color[] cropData = new Color[cropped.Width * cropped.Height];  

            source.GetData<Color>(data);  

            int index = 0;  
            for (int y = area.Y; y < area.Y + area.Height; y++)  
            {  
                for (int x = area.X; x < area.X + area.Width; x++)  
                {  
                    cropData[index] = data[x + (y * source.Width)];  
                    index++;  
                }  
            }  

            cropped.SetData<Color>(cropData);  

            return cropped;  
        }

    }
}
