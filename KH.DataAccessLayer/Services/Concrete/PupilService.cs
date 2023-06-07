using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using KH.DataAccessLayer.Repositories.Concrete;
using KH.DataAccessLayer.Services.Abstract;
using KH.DataAccessLayer.StaticData.Dictionaries;
using KH.DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Services.Concrete
{
    public class PupilService : IService
    {
        private IPupilRepository _pupilRepository;
        private ISectorRepository _sectorRepository;
        public PupilService(IPupilRepository pupilRepository, ISectorRepository sectorRepository)
        {
            _pupilRepository = pupilRepository;
            _sectorRepository = sectorRepository;
        }

        public bool Create(BaseViewModel viewModel)
        {
            var pupil = ConvertToPupilModel((PupilVM)viewModel, new Pupil());
            try
            {
                _pupilRepository.Create(pupil);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(BaseViewModel pupilVM)
        {
            try
            {
                var pupil = _pupilRepository.GetAll().FirstOrDefault(x => x.Id == pupilVM.ID);
                _pupilRepository.Delete(pupil);
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
            return pupils.Cast<object>().ToList();
        }

        public bool Update(BaseViewModel viewModel)
        {
            try
            {
                Pupil pupil = _pupilRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                if (pupil != null)
                {
                    ConvertToPupilModel((PupilVM)viewModel, pupil);
                    _pupilRepository.Update(pupil);
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
                foreach (var viewModel in viewModels.Cast<PupilVM>())
                {
                    Pupil? pupil = _pupilRepository.GetAll().FirstOrDefault(x => x.Id == viewModel.ID);
                    _ = pupil == null ? Create(viewModel) : Update(viewModel);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Pupil ConvertToPupilModel(PupilVM pupilVM, Pupil pupil)
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
        private void Save()
        {
            _pupilRepository.SaveChanges();
        }
    }
}
