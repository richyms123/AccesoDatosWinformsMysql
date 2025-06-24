using AccesoDatosWinForm;
using AccesoDatosWinForm.data;

namespace AccesoDatosWinformsMysql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Categories c = new Categories
            {
                CategoryName = "Futbol",
                Description = "Messi el mejor"
            };
            var conn = new AccesoDatosMySql("localhost", "nwind",
                "root", "700r", 3306);

            var resukt = conn.ejecutarSentencia(
                "INSERT INTO categories (categoryname, description) " +
                "VALUES ('@categoryname', '@description')", c
                );

            MessageBox.Show($"Filas afectas {resukt}");
        }
    }
}
