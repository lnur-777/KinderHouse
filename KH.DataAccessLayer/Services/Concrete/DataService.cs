using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories;
using KH.DataAccessLayer.Repositories.Abstract;
using KH.DataAccessLayer.Repositories.Concrete;
using KH.DataAccessLayer.Services.Abstract;
using KH.DataAccessLayer.StaticData.Dictionaries;
using KH.DataAccessLayer.ViewModels;
using System.Collections;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace KH.DataAccessLayer.Services.Concrete
{
    public class DataService : IDataService
    {
        private readonly ElnurhContext _context;
        private static IPupilRepository? _pupilRepository;
        private static ISectorRepository? _sectorRepository;
        private static IWorkerRepository? _workerRepository;
        private static ILessonRepository? _lessonRepository;
        private static IPositionRepository? _positionRepository;
        private static IPurchaseRepository? _purchaseRepository;
        public DataService()
        {
            _context ??= new();
            _pupilRepository ??= new PupilRepository(_context);
            _sectorRepository ??= new SectorRepository(_context);
            _workerRepository ??= new WorkerRepository(_context);
            _lessonRepository ??= new LessonRepository(_context);
            _positionRepository ??= new PositionRepository(_context);
            _purchaseRepository ??= new PurchaseRepository(_context);
        }
        public List<object>? GetData<T>() where T : class => typeof(T) switch
        {
            var className when className == typeof(PupilVM) => GetPupils(),
            var className when className == typeof(WorkerVM) => GetWorkers(),
            var className when className == typeof(PurchaseVM) => GetPurchases(),
            _ => null,
        };

        private static List<object> GetPupils()
        {
            var pupils = _pupilRepository.GetAll().Select(x => new PupilVM
            {
                ID = x.Id,
                Name = x.Name,
                SurName = x.SurName,
                MotherName = x.MotherName.Replace("\r", "").Replace("\n", ""),
                FatherName = x.FatherName.Replace("\r", "").Replace("\n", ""),
                Birthday = x.Birthday.Value.ToShortDateString(),
                Orientation = x.Orientation,
                RegisterDate = x.RegisterDate.Value.ToShortDateString(),
                ParentMaritalStatus = MaritalStatus.StatusList[(int)x.ParentMaritalStatus],
                SectorName = _sectorRepository.GetAll().FirstOrDefault(z => z.Id == x.SectorId).Name
            });
            return new List<object>(pupils);
        }

        private static List<object> GetPurchases()
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
            return new List<object>(purchases);
        }

        private static List<object> GetWorkers()
        {
            var workers = _workerRepository.GetAll().Select(x => new WorkerVM
            {
                ID = x.Id,
                Name = x.Name,
                SurName = x.SurName,
                FatherName = x.FatherName,
                RegisterDate = x.RegisteredDate.Value.ToShortDateString(),
                Lesson = _lessonRepository.GetAll().FirstOrDefault(z => z.Id == x.LessonId).Name,
                Position = _positionRepository.GetAll().FirstOrDefault(y => y.Id == x.PositionId).Name,
                Salary = x.Salary.ToString(),
                Note = x.Note,
            });
            return new List<object>(workers);
        }

        public bool UpdateData(IEnumerable viewModels, string type, bool isDelete)
        {
            switch (type)
            {
                case var value when value == nameof(PupilVM):
                    return UpdatePupil(viewModels, isDelete);
                case var value when value == nameof(WorkerVM):
                    UpdateWorker(viewModels, isDelete);
                    return false;
                case var value when value == nameof(PurchaseVM):
                    UpdatePurchase(viewModels, isDelete);
                    return false;
                default:
                    return false;
            }
        }

        private static bool UpdatePupil(IEnumerable viewModels, bool isDelete)
        {
            try
            {
                foreach (var viewModel in viewModels.Cast<PupilVM>())
                {
                    Pupil pupil = _pupilRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                    if (pupil != null && !isDelete)
                    {
                        ConvertToPupilModel(viewModel, pupil);
                        _pupilRepository.Update(pupil);
                    }
                    else if (pupil == null && !isDelete)
                    {
                        pupil = ConvertToPupilModel(viewModel, new Pupil());
                        _pupilRepository.Create(pupil);
                    }
                    else
                    {
                        _pupilRepository.Delete(pupil);
                    }
                }
                _pupilRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool UpdateWorker(IEnumerable viewModels, bool isDelete)
        {
            try
            {
                foreach (var viewModel in viewModels.Cast<WorkerVM>())
                {
                    var worker = _workerRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                    if (worker != null && !isDelete)
                    {
                        ConvertToWorkerModel(viewModel, worker);
                        _workerRepository.Update(worker);
                    }
                    else if (worker == null && !isDelete)
                    {
                        worker = ConvertToWorkerModel(viewModel, new Worker());
                        _workerRepository.Update(worker);
                    }
                    else
                    {
                        _workerRepository.Delete(worker);
                    }
                    
                }
                _workerRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private static bool UpdatePurchase(IEnumerable viewModels, bool isDelete)
        {
            try
            {
                foreach (var viewModel in viewModels.Cast<PurchaseVM>())
                {
                    var purchase = _purchaseRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                    if (purchase != null && !isDelete)
                    {
                        ConvertToPurchaseModel(viewModel, purchase);
                        _purchaseRepository.Update(purchase);

                    }
                    else if (purchase == null && !isDelete)
                    {
                        purchase = ConvertToPurchaseModel(viewModel, purchase);
                        _purchaseRepository.Create(purchase);
                    }
                    else
                    {
                        _purchaseRepository.Delete(purchase);
                    }
                }
                _purchaseRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static Pupil ConvertToPupilModel(PupilVM pupilVM, Pupil pupil)
        {
            pupil.Id = pupilVM.ID;
            pupil.Name = pupilVM.Name;
            pupil.SurName = pupilVM.SurName;
            pupil.FatherName = pupilVM.FatherName;
            pupil.MotherName = pupilVM.MotherName;
            pupil.Birthday = Convert.ToDateTime(pupilVM.Birthday);
            pupil.Orientation = pupilVM.Orientation;
            pupil.RegisterDate = Convert.ToDateTime(pupilVM.RegisterDate);
            pupil.ParentMaritalStatus = MaritalStatus.StatusList.FirstOrDefault(x => x.Value == pupilVM.ParentMaritalStatus).Key;
            pupil.SectorId = _sectorRepository.GetAll().FirstOrDefault(z => z.Name == pupilVM.SectorName).Id;
            return pupil;
        }
        private static Worker ConvertToWorkerModel(WorkerVM workerVM, Worker worker)
        {
            worker.Id = workerVM.ID;
            worker.Name = workerVM.Name;
            worker.SurName = workerVM.SurName;
            worker.FatherName = workerVM.FatherName;
            worker.Note = workerVM.Note;
            worker.RegisteredDate = Convert.ToDateTime(workerVM.RegisterDate);
            worker.Lesson = _lessonRepository.GetAll().FirstOrDefault(x => x.Name.Equals(workerVM.Lesson));
            worker.Position = _positionRepository.GetAll().FirstOrDefault(x => x.Name.Equals(workerVM.Position));
            worker.Salary = decimal.Parse(workerVM.Salary);
            return worker;
        }
        private static Purchase ConvertToPurchaseModel(PurchaseVM purchaseVM, Purchase purchase)
        {
            purchase.Pupil = _pupilRepository.GetAll().FirstOrDefault(x => ($"{x.Name} {x.SurName} {x.FatherName}").Equals(purchaseVM.PupilName));
            purchase.Note = purchaseVM.Note;
            purchase.PaidAmount = decimal.Parse(purchaseVM.PaidAmount);
            purchase.Amount = decimal.Parse(purchaseVM.MonthlyAmount);
            return purchase;
        }
    }
}
