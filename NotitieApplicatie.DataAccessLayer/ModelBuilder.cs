using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.DataAccessLayer
{
    internal class ModelBuilder :DropCreateDatabaseIfModelChanges<NotitieDBContext>
        
    {
        protected override void Seed(NotitieDBContext context)
        {
            base.Seed(context);

            List<Eigenaar> eigenaars = new List<Eigenaar>
            {
            new Eigenaar("Piet Pinter", "PietPienter@gmail.com"),
            new Eigenaar("Jan Janssens", "JanJanssens@gmail.com")
        };

           

            List<Categorie> categorieen = new List<Categorie>
            {
             new Categorie("Application Dev","#4D2FCB"),
            new Categorie("Web and Mobile Dev", "#CBBF2F"),
            new Categorie("Engels","#ABDE81"),
            new Categorie("Data Retrieval","#25A5A7"),
            new Categorie("Html Basics", "#C933C0"),
            new Categorie("Frans","#AD24A4")
            };

            List<NotitieBoek> notitieboeken = new List<NotitieBoek>
            {
            new NotitieBoek("Jaar 1 ","Dit is de eerste notitieboek", eigenaars[1]),
            new NotitieBoek("Jaar 2 ","Dit is de tweede notitieboek", eigenaars[0]),
            };

            context.Notities.Add(new Notitie("eerste les PHP", "Dit is een eerste notie van Web & Mobile waar wij ons eigen PHP hebben ontwikkeld. Super tof.", DateTime.UtcNow, DateTime.UtcNow, 2.5, categorieen[1], eigenaars[0], notitieboeken[1]));
            context.Notities.Add(new Notitie("eerste les Tenses", "Dit is een eerste notie van Engels", DateTime.UtcNow, DateTime.UtcNow, 2.5, categorieen[2], eigenaars[0], notitieboeken[1]));
            context.Notities.Add(new Notitie("eerste les ImmoWin", "Dit is een eerste notie van Application Dev", DateTime.UtcNow, DateTime.UtcNow, 5, categorieen[0], eigenaars[0], notitieboeken[1]));
            context.Notities.Add(new Notitie("tweede les API", "Dit is een eerste notie van Web & Mobile", DateTime.UtcNow, DateTime.UtcNow, 4, categorieen[1], eigenaars[0], notitieboeken[1]));
            context.Notities.Add(new Notitie("eerste les Verbs", "Dit is een eerste notie van Engels", DateTime.UtcNow, DateTime.UtcNow, 3.5, categorieen[2], eigenaars[0], notitieboeken[1]));
            context.Notities.Add(new Notitie("eerste les Get and Set", "Dit is een eerste notie van Data Retrieval", DateTime.UtcNow, DateTime.UtcNow, 0.5, categorieen[3], eigenaars[1], notitieboeken[0]));
            context.Notities.Add(new Notitie("eerste les DOM", "Dit is een eerste notie van HTML Basics", DateTime.UtcNow, DateTime.UtcNow, 1.5, categorieen[4], eigenaars[1], notitieboeken[0]));
            context.Notities.Add(new Notitie("eerste les Preposition", "Dit is een eerste notie van Frans I ", DateTime.UtcNow, DateTime.UtcNow, 2.5, categorieen[5], eigenaars[1], notitieboeken[0]));


        }
    }
}
