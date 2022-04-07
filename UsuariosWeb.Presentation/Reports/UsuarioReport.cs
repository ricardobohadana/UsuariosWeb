using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using UsuariosWeb.Domain.Entities;

namespace UsuariosWeb.Presentation.Reports
{
    public class UsuarioReport
    {
        public static byte[] GerarPdf(List<Usuario> usuarios)
        {
            // criando um documento PDF em memória
            MemoryStream memoryStream = new MemoryStream();
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(memoryStream));

            // criando o conteúdo do documento PDF
            using(Document document = new Document(pdfDocument))
            {
                document.Add(new Paragraph("Relatório de Usuários").AddStyle(tituloStyle()));
                document.Add(new Paragraph("Listagem de usuários cadastrados no sistema").AddStyle(subTituloStyle()));
                document.Add(new Paragraph($"Data de geração: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}").AddStyle(textoStyle()));

                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph($"Quantidade de usuários: {usuarios.Count()}").AddStyle(textoStyle()));
                document.Add(new Paragraph("\n"));

                // criando uma tabela
                Table tabela = new Table(3);
                tabela.SetWidth(UnitValue.CreatePercentValue(100));

                tabela.AddHeaderCell("NOME DO USUÁRIO").AddStyle(headerCellStyle());
                tabela.AddHeaderCell("EMAIL DO USUÁRIO").AddStyle(headerCellStyle());
                tabela.AddHeaderCell("DATA HORA DO CADASTRO").AddStyle(headerCellStyle());

                foreach (Usuario usuario in usuarios)
                {
                    tabela.AddCell(new Paragraph(usuario.Nome).AddStyle(cellStyle()));
                    tabela.AddCell(new Paragraph(usuario.Email).AddStyle(cellStyle()));
                    tabela.AddCell(new Paragraph(usuario.DataCadastro.ToString("dd/MM/yyyy HH:mm")).AddStyle(cellStyle()));
                }

                // adicionando a tabela ao pdf
                document.Add(tabela);
            }

            return memoryStream.ToArray();
        }

        private static Style tituloStyle()
        {
            Style style = new Style();

            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            style.SetFontSize(24);
            style.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 102, 204)));

            return style;
        }

        private static Style subTituloStyle()
        {
            Style style = new Style();

            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            style.SetFontSize(16);
            style.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 102, 204)));

            return style;
        }

        private static Style textoStyle()
        {
            Style style = new Style();

            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            style.SetFontSize(14);

            return style;
        }

        private static Style headerCellStyle()
        {
            Style style = new Style();

            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            style.SetFontSize(13);
            style.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 102, 204)));

            return style;
        }

        private static Style cellStyle()
        {
            Style style = new Style();

            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            style.SetFontSize(10);
            style.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 0, 0)));

            return style;
        }

    }
}
