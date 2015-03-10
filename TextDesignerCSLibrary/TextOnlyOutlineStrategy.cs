using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public class TextOnlyOutlineStrategy : ITextStrategy
    {
	    public TextOnlyOutlineStrategy()
        {
            m_nThickness=2;
            m_bRoundedEdge = false;
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

	    ~TextOnlyOutlineStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            TextOnlyOutlineStrategy p = new TextOnlyOutlineStrategy();
            p.Init(m_clrOutline, m_nThickness, m_bRoundedEdge);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Drawing.Color clrOutline, 
		    int nThickness,
            bool bRoundedEdge)
        {
            m_clrOutline = clrOutline;
            m_nThickness = nThickness; 
            m_bRoundedEdge = bRoundedEdge;
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

                using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                {
                    pen.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen, path);
                }
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

                using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                {
                    pen.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen, path);
                }
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
                b = GDIPath.ConvertToPixels(graphics, m_nThickness, 0.0f, ref fStartX2, ref fStartY2, ref pixelThick, ref pixelThick2);

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
                b = GDIPath.ConvertToPixels(graphics, m_nThickness, 0.0f, ref fStartX2, ref fStartY2, ref pixelThick, ref pixelThick2);

                if (false == b)
                    return false;

                fDestWidth += pixelThick;
                fDestHeight += pixelThick;
            }
	        return true;
        }


	    protected System.Drawing.Color m_clrOutline;
	    protected int m_nThickness;
        protected bool m_bClrText;
        protected bool m_bRoundedEdge;
        protected bool disposed;
    }
}
