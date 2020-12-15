using NotitieApplicatie.DataAccessLayer;
using System.Windows;


namespace NotitieApplicatie.Views
{

    public partial class NotitieAppliciatieMainView : Window
    {
     
        public NotitieAppliciatieMainView()
        {
        

            InitializeComponent();

            /// <summary>
            /// Dit geeft een MessageBox weer aan de gebruiker voor dat hij binnen de applicatie scherm geraak.
            /// Hier geeft hij een keuze door of hij de databank wilt laten heropstarten of niet
            /// Deze result wordt dan doorgeven in de constructor van onze DbContext class die een overload constructor heeft die een boleean als parameter vergt.
            /// Daar wordt dan bepaald of wij de DB opnieuw maken of gewoon de huidige gemaakte gebruiken.
            /// 
            /// </summary>

            //TODO: Zoek een manier om dit MVVM te maken ?
                       
            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to restart the DataBase ?\nThis action will erase all non-seed records.", "Important Question", MessageBoxButton.YesNo);

            if(result.ToString() == "No")
            {
            new NotitieDBContext(false);
               


            
            }
            else
            {
              new NotitieDBContext(true);
            }
           
        }
    }
}
