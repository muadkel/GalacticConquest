//=================================================
// xWinForms
// Copyright ?2007 by Eric Grossinger
// http://psycad007.spaces.live.com/
//=================================================
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GalacticConquest
{
    public class Scrollbar : Control
    {
        #region Fields
        Button scrollUp, scrollDown;
        Texture2D barTex, scrollerTex;

        public Point size = Point.Zero;
        Vector2 scrollerPosition = Vector2.Zero;

        //Style style = Style.Default;

        public Axis axis = Axis.Vertical;
        public enum Axis
        {
            Horizontal,
            Vertical
        }

        MouseState ms;
        Point mousePosition;
        bool bIsScrolling = false;
        bool bMousePressed = false;
        Rectangle scrollerArea = Rectangle.Empty;
        Rectangle barArea = Rectangle.Empty;
        int scrollStart = 0;

        int tick = 1;
        int pageSize = 5;

        public event EventHandler OnValueChange;
        #endregion

        #region Construction
        public Scrollbar(string name, Vector2 position, Axis axis, Nullable<int> width, Nullable<int> height, int max, int value)//, Style style)
        {
            this.Type = ControlType.Scrollbar;
            this.name = name;
            this.position = position;
            this.axis = axis;
            //this.style = style;

            this.min = 0;
            this.max = max;
            this.value = value;

            switch (axis)
            {
                case Axis.Horizontal:
                    if (width.HasValue)
                        size.X = width.Value;
                    break;
                case Axis.Vertical:
                    if (height.HasValue)
                        size.Y = height.Value;
                    break;
            }

            Init();

            scrollUp.OnMousePress += new EventHandler(On_ScrollUp);
            scrollDown.OnMousePress += new EventHandler(On_ScrollDown);
            OnValueChange += new EventHandler(onValueChange);
        }
        #endregion

        #region Initial
        private void Init()
        {
            switch (axis)
            {
                case Axis.Horizontal:
                    scrollUp = new Button("btScrollUp", "scroll_left", position, new Color(new Vector4(0.9f, 0.9f, 0.9f, 1f)));//, /*style,*/ true );
                    scrollDown = new Button("btScrollDown", "scroll_right", position + new Vector2(size.X - scrollUp.size.Y, 0f), new Color(new Vector4(0.9f, 0.9f, 0.9f, 1f)));
                    size.Y = scrollUp.size.Y;
                    break;
                case Axis.Vertical:
                    scrollUp = new Button("btScrollUp", "scroll_up", position, new Color(new Vector4(0.9f, 0.9f, 0.9f, 1f)));//, /*style,*/ true );
                    scrollDown = new Button("btScrollDown", "scroll_down", position + new Vector2(0f, size.Y - scrollUp.size.Y), new Color(new Vector4(0.9f, 0.9f, 0.9f, 1f)));//,/* style,*/ true );
                    size.X = scrollUp.size.X;
                    break;
            }

            CreateTexture();
            RedrawScroller();
        }

        private void CreateTexture()
        {
            Color[] pixel = new Color[0];

            switch (axis)
            {
                case Axis.Horizontal:
                    barTex = new Texture2D(BaseGame.Device, size.X - (scrollUp.size.X + scrollDown.size.X), size.Y, 1, Control.resourceUsage, SurfaceFormat.Color);
                    pixel = new Color[(size.X - (scrollUp.size.X + scrollDown.size.X)) * size.Y];
                    for (int y = 0; y < size.Y; y++)
                    {
                        for (int x = 0; x < size.X - (scrollUp.size.X + scrollDown.size.X); x++)
                        {
                            if (y < 2 || y > size.Y - 3)
                                pixel[x + y * (size.X - (scrollUp.size.X + scrollDown.size.X))] = new Color(new Vector4(0.7f, 0.7f, 0.7f, 1f));
                            else
                                pixel[x + y * (size.X - (scrollUp.size.X + scrollDown.size.X))] = new Color(new Vector4(0.75f, 0.75f, 0.75f, 1f));
                        }
                    }
                    break;
                case Axis.Vertical:
                    barTex = new Texture2D(BaseGame.Device, size.X, size.Y - (scrollUp.size.Y + scrollDown.size.Y), 1, Control.resourceUsage, SurfaceFormat.Color);
                    pixel = new Color[size.X * (size.Y - (scrollUp.size.Y + scrollDown.size.Y))];
                    for (int y = 0; y < size.Y - (scrollUp.size.Y + scrollDown.size.Y); y++)
                    {
                        for (int x = 0; x < size.X; x++)
                        {
                            if (x < 2 || x > size.X - 3)
                                pixel[x + y * size.X] = new Color(new Vector4(0.7f, 0.7f, 0.7f, 1f));
                            else
                                pixel[x + y * size.X] = new Color(new Vector4(0.75f, 0.75f, 0.75f, 1f));
                        }
                    }
                    break;
            }

            barTex.SetData<Color>(pixel);
        }

        #endregion

        #region Update

        private void RedrawScroller()
        {
            int sizeX = size.X;
            int sizeY = size.Y;

            switch (axis)
            {
                case Axis.Horizontal:
                    sizeX = (int)(size.X / System.Math.Min(max, size.X / 20));
                    sizeY -= 2;
                    break;
                case Axis.Vertical:
                    sizeY = (int)(size.Y / System.Math.Min(max, size.Y / 20));
                    sizeX -= 2;
                    break;
            }

            scrollerTex = new Texture2D(BaseGame.Device, sizeX, sizeY, 1, Control.resourceUsage, SurfaceFormat.Color);

            Color[] pixel = new Color[sizeX * sizeY];

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    pixel[x + y * sizeX] = new Color(new Vector4(0.7f, 0.7f, 0.7f, 1f));

                    float cX = pixel[x + y * sizeX].ToVector3().X;
                    float cY = pixel[x + y * sizeX].ToVector3().X;
                    float cZ = pixel[x + y * sizeX].ToVector3().X;

                    if (x < 2 || y < 2)
                    {
                        cX *= 1.15f;
                        cY *= 1.15f;
                        cZ *= 1.15f;
                    }
                    else if (x > sizeX - 3 || y > sizeY - 3)
                    {
                        cX *= .75f;
                        cY *= .75f;
                        cZ *= .75f;
                    }

                    pixel[x + y * sizeX] = new Color(new Vector4(cX, cY, cZ, 1f));
                }
            }

            scrollerTex.SetData<Color>(pixel);
        }

        public void On_ScrollUp(object obj, EventArgs e)
        {
            value -= tick;
            if (value < 0)
                value = 0;
            OnValueChange(this, EventArgs.Empty);
        }

        public void On_ScrollDown(object obj, EventArgs e)
        {
            value += tick;
            if (value > max)
                value = max;
            OnValueChange(this, EventArgs.Empty);
        }

        private void onValueChange(object obj, EventArgs e)
        {
        }

        public override void Update()//Vector2 formPosition, Vector2 formSize )
        {
            //base.Update( formPosition, formSize );

            scrollUp.Update();// position, formSize );

            ms = Mouse.GetState();
            mousePosition.X = ms.X;
            mousePosition.Y = ms.Y;

            scrollerArea = new Rectangle((int)scrollerPosition.X, (int)scrollerPosition.Y, scrollerTex.Width, scrollerTex.Height);

            if (/*!Form.isInUse*/ benable)
            {
                switch (axis)
                {
                    case Axis.Horizontal:
                        //CheckHorizontalScrolling();
                        //if (!bIsScrolling)
                        CheckHorizontalBar();
                        break;
                    case Axis.Vertical:
                        //CheckVerticalScrolling();
                        //if (!bIsScrolling)
                        CheckVerticalBar();
                        break;
                }
            }

            UpdateScroller();// formSize );

            //CheckVisibility( formPosition, formSize );
        }

        private void UpdateScroller()//Vector2 formSize )
        {
            float percentage = (float)value / max;

            switch (axis)
            {
                case Axis.Horizontal:

                    if (scrollerTex.Width != size.X / max)
                        RedrawScroller();

                    scrollDown.Update();// position + new Vector2( size.X - scrollUp.size.X, 0f ), formSize );
                    //scrollDown.position += new Vector2( size.X - scrollUp.size.X, 0f );

                    float sizeX = size.X - (scrollUp.size.X + scrollDown.size.X + scrollerTex.Width);
                    float posX = sizeX * percentage;

                    if (!bIsScrolling)
                        scrollerPosition = new Vector2(position.X + scrollUp.size.X + posX, position.Y + 1f);
                    else
                        scrollerPosition = new Vector2(position.X + scrollUp.size.X + posX + (mousePosition.X - scrollStart), position.Y + 1f);

                    if (scrollerPosition.X < position.X + scrollUp.size.X)
                        scrollerPosition.X = position.X + scrollUp.size.X;
                    else if (scrollerPosition.X > position.X + size.X - (scrollUp.size.X + scrollerTex.Width))
                        scrollerPosition.X = position.X + size.X - (scrollUp.size.X + scrollerTex.Width);

                    break;

                case Axis.Vertical:

                    if (scrollerTex.Height != size.Y / max)
                        RedrawScroller();

                    scrollDown.Update();// position + new Vector2( 0f, size.Y - scrollUp.size.Y ), formSize );
                    //scrollDown.position += new Vector2( 0f, size.Y - scrollUp.size.Y );
                    float sizeY = size.Y - (scrollUp.size.Y + scrollDown.size.Y + scrollerTex.Height);
                    float posY = sizeY * percentage;

                    if (!bIsScrolling)
                        scrollerPosition = new Vector2(position.X + 1f, position.Y + scrollUp.size.Y + posY);
                    else
                        scrollerPosition = new Vector2(position.X + 1f, position.Y + scrollUp.size.Y + posY + (mousePosition.Y - scrollStart));

                    if (scrollerPosition.Y < position.Y + scrollUp.size.Y)
                        scrollerPosition.Y = position.Y + scrollUp.size.Y;
                    else if (scrollerPosition.Y > position.Y + size.Y - (scrollUp.size.Y + scrollerTex.Height))
                        scrollerPosition.Y = position.Y + size.Y - (scrollUp.size.Y + scrollerTex.Height);

                    break;
            }
        }

        private void CheckVisibility(Vector2 formPosition, Vector2 formSize)
        {
            if (position.X + size.X > formPosition.X + formSize.X - 15f)
                bVisible = false;
            else if (position.Y + size.Y > formPosition.Y + formSize.Y - 25f)
                bVisible = false;
            else
                bVisible = true;
        }

        private void CheckVerticalScrolling()
        {
            //if (Math.isInRectangle( mousePosition, scrollerArea ))
            //{
            //    //FIX ME
            //}
        }

        private void CheckVerticalBar()
        {
            barArea = new Rectangle((int)position.X, (int)position.Y + scrollUp.size.Y, size.X, size.Y - (scrollUp.size.Y + scrollDown.size.Y));

            //if (Math.isInRectangle( mousePosition, barArea ) && !Math.isInRectangle( mousePosition, scrollerArea ))
            if (barArea.Contains(mousePosition) && !scrollerArea.Contains(mousePosition))
            {
                if (ms.LeftButton == ButtonState.Pressed && !bMousePressed)
                {
                    bMousePressed = true;
                    if (mousePosition.Y < scrollerPosition.Y)
                        PageUp();
                    else
                        PageDown();
                    OnValueChange(this, EventArgs.Empty);
                }
                else if (ms.LeftButton == ButtonState.Released && bMousePressed)
                    bMousePressed = false;
            }
            else if (ms.LeftButton == ButtonState.Released && bMousePressed)
                bMousePressed = false;
        }

        private void CheckHorizontalScrolling()
        {
            //if (Math.isInRectangle( mousePosition, scrollerArea ))
            //{
            //    //FIX ME
            //}
        }

        private void CheckHorizontalBar()
        {
            barArea = new Rectangle((int)position.X + scrollUp.size.X, (int)position.Y, size.X - (scrollUp.size.X + scrollDown.size.X), size.Y);

            //if (Math.isInRectangle( mousePosition, barArea ) && !Math.isInRectangle( mousePosition, scrollerArea ))
            if (barArea.Contains(mousePosition) && !scrollerArea.Contains(mousePosition))
            {
                if (ms.LeftButton == ButtonState.Pressed && !bMousePressed)
                {
                    bMousePressed = true;
                    if (mousePosition.X < scrollerPosition.X)
                        PageUp();
                    else
                        PageDown();
                    OnValueChange(this, EventArgs.Empty);
                }
                else if (ms.LeftButton == ButtonState.Released && bMousePressed)
                    bMousePressed = false;
            }
            else if (ms.LeftButton == ButtonState.Released && bMousePressed)
                bMousePressed = false;
        }

        public void PageUp()
        {
            value -= pageSize;
            if (value < min)
                value = min;
        }

        public void PageDown()
        {
            value += pageSize;
            if (value > max)
                value = max;
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch, float alpha)
        {
            Color dynamicColor = new Color(new Vector4(1f, 1f, 1f, alpha));

            switch (axis)
            {
                case Axis.Horizontal:
                    spriteBatch.Draw(barTex, position + new Vector2(scrollUp.size.X, 0f), null, dynamicColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                    break;
                case Axis.Vertical:
                    spriteBatch.Draw(barTex, position + new Vector2(0f, scrollUp.size.Y), null, dynamicColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                    break;
            }

            scrollUp.Draw(spriteBatch, alpha);
            scrollDown.Draw(spriteBatch, alpha);

            if (max > min)
                spriteBatch.Draw(scrollerTex, scrollerPosition, null, dynamicColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);//dynamicColor );
        }
        #endregion
    }
}
