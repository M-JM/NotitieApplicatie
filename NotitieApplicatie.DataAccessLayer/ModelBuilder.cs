using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.DataAccessLayer
{
    internal class ModelBuilder :DropCreateDatabaseAlways <NotitieDBContext>
        
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
            new Categorie("Application Dev","Black"),
            new Categorie("Web and Mobile Dev", "Blue"),
            new Categorie("Engels","Pink"),
            new Categorie("Data Retrieval","Red"),
            new Categorie("Html Basics", "Yellow"),
            new Categorie("Frans","Orange")
            };

            List<NotitieBoek> notitieboeken = new List<NotitieBoek>
            {
            new NotitieBoek("Jaar 1 ","Dit is de eerste notitieboek", eigenaars[0]),
            new NotitieBoek("Jaar 2 ","Dit is de tweede notitieboek", eigenaars[0]),
            };
            
            context.Notities.Add(new Notitie("eerste les", "Dit is een eerste notie van Application Dev", DateTime.UtcNow, DateTime.UtcNow, categorieen[0], eigenaars[0],notitieboeken[1]));
            context.Notities.Add(new Notitie("eerste les", "Dit is een eerste notie van Web & Mobile", DateTime.UtcNow, DateTime.UtcNow, categorieen[1], eigenaars[0], notitieboeken[1]));
            context.Notities.Add(new Notitie("eerste les", "Dit is een eerste notie van Engels Dev", DateTime.UtcNow, DateTime.UtcNow, categorieen[2], eigenaars[0], notitieboeken[1]));
            context.Notities.Add(new Notitie("eerste les", "Dit is een eerste notie van Data Retrieval", DateTime.UtcNow, DateTime.UtcNow, categorieen[3], eigenaars[1], notitieboeken[0]));
            context.Notities.Add(new Notitie("eerste les", "Dit is een eerste notie van HTML Basics", DateTime.UtcNow, DateTime.UtcNow, categorieen[4], eigenaars[1], notitieboeken[0]));
            context.Notities.Add(new Notitie("eerste les", "Dit is een eerste notie van Frans I ", DateTime.UtcNow, DateTime.UtcNow, categorieen[5], eigenaars[1], notitieboeken[0]));
            
            }
    }
}
