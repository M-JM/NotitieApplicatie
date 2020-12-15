using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.BusinessAccessLayer
{
    public interface IDbRepository 
    {
        Notitie AddNotitie(Notitie notitie);
        NotitieBoek AddNotitieBoek(NotitieBoek notitieBoek);
        Categorie AddCategorie(Categorie categorie);
        NotitieBoek NotitieboekById(int id);
        Notitie NotitieById(int id);
        FullObservableCollection<Categorie> Categorieenlijst();
        Notitie UpdateNotitie(Notitie notitie);
        NotitieBoek UpdateNotitieBoek(NotitieBoek notitieBoek);
        Categorie UpdateCategorie(Categorie categorie);
        Boolean RemoveNotitie(Notitie notitie);
        Boolean RemoveNotitieBoek(NotitieBoek notitieBoek);
        Boolean RemoveCategorie(Categorie categorie);
        FullObservableCollection<Eigenaar> Eigenaarslijst();
        FullObservableCollection<NotitieBoek> Notitieboeklijst();
        ObservableCollection<Notitie> Notitielijst();
    }
}
