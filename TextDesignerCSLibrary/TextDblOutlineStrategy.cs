using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public class TextDblOutlineStrategy : ITextStrategy
    {
	    public TextDblOutlineStrategy()
        {
            m_nThickness1=2;
            m_nThickness2=2;
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
        ~TextDblOutlineStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            TextDblOutlineStrategy p = new TextDblOutlineStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline1, m_clrOutline2, m_nThickness1, m_nThickness2);
            else
                p.Init(m_brushText, m_clrOutline1, m_clrOutline2, m_nThickness1, m_nThickness2);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Drawing.Color clrText, 
		    System.Drawing.Color clrOutline1, 
		    System.Drawing.Color clrOutline2, 
		    int nThickness1,
		    int nThickness2 )
        {
            m_clrText = clrText;
            m_bClrText = true;
            m_clrOutline1 = clrOutline1;
            m_clrOutline2 = clrOutline2;
            m_nThickness1 = nThickness1; 
            m_nThickness2 = nThickness2; 
        }

        public void Init(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness1,
            int nThickness2)
        {
            m_brushText = brushText;
            m_bClrText = false;
            m_clrOutline1 = clrOutline1;
            m_clrOutline2 = clrOutline2;
            m_nThickness1 = nThickness1;
            m_nThickness2 = nThickness2;
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

                using (Pen pen2 = new Pen(m_clrOutline2, m_nThickness1 + m_nThickness2))
                {
                    pen2.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen2, path);
                }
                using (Pen pen1 = new Pen(m_clrOutline1, m_nThickness1))
                {
                    pen1.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen1, path);
                }

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

                using (Pen pen2 = new Pen(m_clrOutline2, m_nThickness1 + m_nThickness2))
                {
                    pen2.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen2, path);
                }
                using (Pen pen1 = new Pen(m_clrOutline1, m_nThickness1))
                {
                    pen1.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen1, path);
                }

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

                if (false == b)
                    return false;

                float pixelThick = 0.0f;
                float pixelThick2 = 0.0f;
                float fStartX2 = 0.0f;
                float fStartY2 = 0.0f;
                b = GDIPath.ConvertToPixels(graphics, m_nThickness1 + m_nThickness2, 0.0f, ref fStartX2, ref fStartY2, ref pixelThick, ref pixelThick2);

                if (false == b)
                    return false;

                fDestWidth += pixelThick;
                fDestHeight += pixelThick;
            }
	        return true;
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

                if (false == b)
                    return false;

                float pixelThick = 0.0f;
                float pixelThick2 = 0.0f;
                float fStartX2 = 0.0f;
                float fStartY2 = 0.0f;
                b = GDIPath.ConvertToPixels(graphics, m_nThickness1 + m_nThickness2, 0.0f, ref fStartX2, ref fStartY2, ref pixelThick, ref pixelThick2);

                if (false == b)
                    return false;

                fDestWidth += pixelThick;
                fDestHeight += pixelThick;
            }
	        return true;
        }


	    protected System.Drawing.Color m_clrText;
	    protected System.Drawing.Color m_clrOutline1;
	    protected System.Drawing.Color m_clrOutline2;
	    protected int m_nThickness1;
	    protected int m_nThickness2;
        protected System.Drawing.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
