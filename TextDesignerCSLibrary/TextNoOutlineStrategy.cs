using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public class TextNoOutlineStrategy : ITextStrategy
    {
	    public TextNoOutlineStrategy()
        {
            m_brushText = null;
            m_bClrText = true;
            disposed = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }

                disposed = true;
            }
        }
	    ~TextNoOutlineStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            TextNoOutlineStrategy p = new TextNoOutlineStrategy();
            if (m_bClrText)
                p.Init(m_clrText);
            else
                p.Init(m_brushText);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Drawing.Color clrText)
        {
            m_clrText = clrText;
            m_bClrText = true;
        }

        public void Init(
            System.Drawing.Brush brushText)
        {
            m_brushText = brushText;
            m_bClrText = false;
        }

        public bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(strText, fontFamily, (int)fontStyle, fontSize, ptDraw, strFormat);

                if (m_bClrText)
                {
                    using (SolidBrush brush = new SolidBrush(m_clrText))
                    {
                        graphics.FillPath(brush, path);
                    }
                }
                else
                    graphics.FillPath(m_brushText, path);
            }
	        return true;
        }


        public bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(strText, fontFamily, (int)fontStyle, fontSize, rtDraw, strFormat);

                if (m_bClrText)
                {
                    using (SolidBrush brush = new SolidBrush(m_clrText))
                    {
                        graphics.FillPath(brush, path);
                    }
                }
                else
                    graphics.FillPath(m_brushText, path);
            }
	        return true;
        }


        public bool MeasureString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat,
            ref float fStartX,
            ref float fStartY,
            ref float fDestWidth,
            ref float fDestHeight)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(strText, fontFamily, (int)fontStyle, fontSize, ptDraw, strFormat);

                fDestWidth = ptDraw.X;
                fDestHeight = ptDraw.Y;
                bool b = GDIPath.MeasureGraphicsPath(graphics, path, ref fStartX, ref fStartY, ref fDestWidth, ref fDestHeight);
	            return b;
            }
        }

        public bool MeasureString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat,
            ref float fStartX,
            ref float fStartY,
            ref float fDestWidth,
            ref float fDestHeight)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(strText, fontFamily, (int)fontStyle, fontSize, rtDraw, strFormat);

                fDestWidth = rtDraw.Width;
                fDestHeight = rtDraw.Height;
                bool b = GDIPath.MeasureGraphicsPath(graphics, path, ref fStartX, ref fStartY, ref fDestWidth, ref fDestHeight);

                return b;
            }
        }

	    protected System.Drawing.Color m_clrText;
        protected System.Drawing.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
