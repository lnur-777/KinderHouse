using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.StaticData
{
    public static class Constants
    {
        public readonly static string[] PupilGridHeaderTexts = {
            "Sil",
            "ID", 
            "Adı", 
            "Soyadı", 
            "Ana adı", 
            "Ata adı", 
            "Doğum tarixi", 
            "Cins", 
            "Qeydiyyat tarixi", 
            "Sector", 
            "Valideynlərin nikah statusu"
        };

        public readonly static string[] WorkerGridHeaderTexts = {
            "Sil",
            "ID",
            "Adı",
            "Soyadı",
            "Ata adı",
            "Vəzifə",
            "Dərs",
            "Əmək haqqı",
            "Qeydiyyat tarixi",
            "Qeyd"
        };

        public readonly static string[] PurchaseGridHeaderTexts = {
            "Sil",
            "ID",
            "Şagirdin adı",
            "Ödənilmiş məbləğ",
            "Abunə haqqı",
            "Ödəniş tarixi",
            "Qeyd"
        };
    }
}
