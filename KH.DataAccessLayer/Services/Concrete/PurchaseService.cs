using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using KH.DataAccessLayer.Services.Abstract;
using KH.DataAccessLayer.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Services.Concrete
{
    public class PurchaseService : IService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPupilRepository _pupilRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository, IPupilRepository pupilRepository)
        {
            _purchaseRepository = purchaseRepository;
            _pupilRepository = pupilRepository;
        }

        public bool Create(BaseViewModel viewModel)
        {
            var purchase = ConvertToPurchaseModel((PurchaseVM)viewModel, new Purchase());
            try
            {
                _purchaseRepository.Create(purchase);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(BaseViewModel purchaseVM)
        {
            try
            {
                var purchase = _purchaseRepository.GetAll().FirstOrDefault(x => x.Id == purchaseVM.ID);
                _purchaseRepository.Delete(purchase);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<object> GetAll()
        {
            var purchases = _purchaseRepository.GetAll().Select(x => new PurchaseVM
            {
                ID = x.Id,
                PupilName = _pupilRepository.GetAll().Where(z => z.Id == x.PupilId).Select(y => $"{y.Name} {y.SurName} {y.FatherName}").ToString(),
                PaidAmount = x.PaidAmount.ToString(),
                MonthlyAmount = x.Amount.ToString(),
                Date = x.Date.Value.ToShortDateString(),
                Note = x.Note
            });
            return purchases.Cast<object>().ToList();
        }

        public bool Update(BaseViewModel viewModel)
        {
            try
            {
                var purchase = _purchaseRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                if (purchase != null)
                {
                    ConvertToPurchaseModel((PurchaseVM)viewModel, purchase);
                    _purchaseRepository.Update(purchase);
                }
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAll(IEnumerable viewModels)
        {
            try
            {
                foreach (var viewModel in viewModels.Cast<PurchaseVM>())
                {
                    Purchase? purchase = _purchaseRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                    _ = purchase == null ? Create(viewModel) : Update(viewModel);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Purchase ConvertToPurchaseModel(PurchaseVM purchaseVM, Purchase purchase)
        {
            purchase.Pupil = _pupilRepository.GetAll().FirstOrDefault(x => ($"{x.Name} {x.SurName} {x.FatherName}").Equals(purchaseVM.PupilName));
            purchase.Note = purchaseVM.Note;
            purchase.PaidAmount = decimal.Parse(purchaseVM.PaidAmount);
            purchase.Amount = decimal.Parse(purchaseVM.MonthlyAmount);
            return purchase;
        }

        private void Save()
        {
            _purchaseRepository.SaveChanges();
        }
    }
}
