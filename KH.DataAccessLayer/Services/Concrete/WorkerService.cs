using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using KH.DataAccessLayer.Repositories.Concrete;
using KH.DataAccessLayer.Services.Abstract;
using KH.DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Services.Concrete
{
    public class WorkerService : IService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IPositionRepository _positionRepository;

        public WorkerService(IWorkerRepository workerRepository, ILessonRepository lessonRepository, IPositionRepository positionRepository)
        {
            _workerRepository = workerRepository;
            _lessonRepository = lessonRepository;
            _positionRepository = positionRepository;
        }

        public bool Create(BaseViewModel viewModel)
        {
            var worker = ConvertToWorkerModel((WorkerVM)viewModel, new Worker());
            try
            {
                _workerRepository.Update(worker);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(BaseViewModel workerVM)
        {
            try
            {
                var worker = _workerRepository.GetAll().FirstOrDefault(x => x.Id == workerVM.ID);
                _workerRepository.Delete(worker);
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
            return workers.Cast<object>().ToList();
        }
        public bool Update(BaseViewModel viewModel)
        {
            try
            {
                var worker = _workerRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                if (worker != null)
                {
                    ConvertToWorkerModel((WorkerVM)viewModel, worker);
                    _workerRepository.Update(worker);
                    Save();
                }
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
                foreach (var viewModel in viewModels.Cast<WorkerVM>())
                {
                    Worker? worker = _workerRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                    _ = worker == null ? Create(viewModel) : Update(viewModel);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Worker ConvertToWorkerModel(WorkerVM workerVM, Worker worker)
        {
            worker.Id = workerVM.ID;
            worker.Name = workerVM.Name;
            worker.SurName = workerVM.SurName;
            worker.FatherName = workerVM.FatherName;
            worker.Note = workerVM.Note;
            worker.RegisteredDate = Convert.ToDateTime(workerVM.RegisterDate);
            worker.Lesson = _lessonRepository.GetAll().FirstOrDefault(x => x.Name.Equals(workerVM.Lesson));
            worker.Position = _positionRepository.GetAll().FirstOrDefault(x => x.Name.Equals(workerVM.Position));
            worker.Salary = decimal.Parse(workerVM.Salary ?? "0");
            return worker;
        }

        private void Save()
        {
            _workerRepository.SaveChanges();
        }
    }
}
