using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories;
using KH.DataAccessLayer.Repositories.Abstract;
using KH.DataAccessLayer.Repositories.Concrete;
using KH.DataAccessLayer.Services.Abstract;
using KH.DataAccessLayer.StaticData.Dictionaries;
using KH.DataAccessLayer.ViewModels;
using System.Collections;
using System.Collections.ObjectModel;

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
            _context = new();
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
            var pupils = _pupilRepository.GetPupils().Select(x => new PupilVM
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
                SectorName = _sectorRepository.GetSectors().FirstOrDefault(z => z.Id == x.SectorId).Name
            });
            return new List<object>(pupils);
        }

        private static List<object> GetPurchases()
        {
            var purchases = _purchaseRepository.GetPurchases().Select(x => new PurchaseVM
            {
                ID = x.Id,
                PupilName = _pupilRepository.GetPupils().Where(z => z.Id == x.PupilId).Select(y => $"{y.Name} {y.SurName} {y.FatherName}").ToString(),
                PaidAmount = x.PaidAmount.ToString(),
                MonthlyAmount = x.Amount.ToString(),
                Date = x.Date.Value.ToShortDateString(),
                Note = x.Note
            });
            return new List<object>(purchases);
        }

        private static List<object> GetWorkers()
        {
            var workers = _workerRepository.GetWorkers().Select(x => new WorkerVM
            {
                ID = x.Id,
                Name = x.Name,
                SurName = x.SurName,
                FatherName = x.FatherName,
                RegisterDate = x.RegisteredDate.Value.ToShortDateString(),
                Lesson = _lessonRepository.GetLessons().FirstOrDefault(z => z.Id == x.LessonId).Name,
                Position = _positionRepository.GetPositions().FirstOrDefault(y => y.Id == x.PositionId).Name,
                Salary = x.Salary.ToString(),
                Note = x.Note,
            });
            return new List<object>(workers);
        }

        public void UpdateData(IEnumerable viewModels, string type)
        {
            switch (type)
            {
                case var value when value == nameof(PupilVM):
                    UpdatePupil(viewModels);
                    break;
                default:
                    break;
            }
        }

        private static void UpdatePupil(IEnumerable viewModels)
        {
            foreach (var model in viewModels.Cast<PupilVM>())
            {
                var pupil = _pupilRepository.GetPupils().FirstOrDefault(x => x.Id == model.ID);
                if (pupil != null)
                {
                    pupil.Name = model.Name;
                    pupil.SurName = model.SurName;
                    pupil.FatherName = model.FatherName;
                    pupil.MotherName = model.MotherName;
                    pupil.Birthday = Convert.ToDateTime(model.Birthday);
                    pupil.Orientation = model.Orientation;
                    pupil.RegisterDate = Convert.ToDateTime(model.RegisterDate);
                    pupil.ParentMaritalStatus = MaritalStatus.StatusList.FirstOrDefault(x => x.Value == model.ParentMaritalStatus).Key;
                    pupil.SectorId = _sectorRepository.GetSectors().FirstOrDefault(z => z.Name == model.SectorName).Id;
                    _pupilRepository.SaveChanges();
                }
            }
        }

    }
}
