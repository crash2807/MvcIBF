using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.StarVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class StarRepository : Repository<Star>, IStarRepository
    {

        private MvcIBFContext _db;
        public StarRepository(MvcIBFContext db): base(db)
        {
            _db = db;
        }

        public void AddStarWithCountry(CreateStarVM vm)
        {
            var star = new Star
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                OtherName = vm.OtherName,
                DateOfBirth = vm.DateOfBirth,
                DateOfDeath = vm.DateOfDeath,
                ProfilePictureURL = vm.ProfilePictureURL,
                CountryId=vm.SelectedCountry
            };
            _db.Stars.Add(star);
            _db.SaveChanges();
            if (vm.SelectedCountry != null)
            {
                var country = _db.Countries.Where(x => x.CountryId == vm.SelectedCountry).FirstOrDefault();
                star.Country = country;
                star.CountryId = country.CountryId;
                country.Stars.Add(star);
                _db.SaveChanges();
            }
        }

        public StarVM GetStarVM(int id)
        {
            StarVM star = _db.Stars.Where(x => x.StarId == id).Select(x => new StarVM()
            {
                StarId = x.StarId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ProfilePictureURL = x.ProfilePictureURL,
                DateOfBirth = x.DateOfBirth,
                DateOfDeath = x.DateOfDeath,
                OtherName = x.OtherName,
                SelectedCountry = x.CountryId,
                CountryName = x.Country.CountryName
            }).First();
                
            
            return star;
            
        }
        public void AddProperties(StarVM vm, Star star)
        {
            if(vm.FirstName != star.FirstName)
            {
                star.FirstName = vm.FirstName;
            }
            if(vm.LastName != star.LastName)
            {
                star.LastName = vm.LastName;
                
            }
            if (vm.OtherName != star.OtherName)
            {
                star.OtherName= vm.OtherName;
            }
            if(vm.ProfilePictureURL != star.ProfilePictureURL)
            {
                star.ProfilePictureURL=vm.ProfilePictureURL;
            }
            if(vm.DateOfBirth != star.DateOfBirth)
            {
                star.DateOfBirth=vm.DateOfBirth;
            }
            if(vm.DateOfDeath != star.DateOfDeath)
            {
                star.DateOfDeath = vm.DateOfDeath;
            }
            _db.Update(star);
            _db.SaveChanges();
            if (vm.SelectedCountry != star.CountryId)
            {
                var oldCountry = _db.Countries.Where(x => x.CountryId == star.CountryId).First();
                var country = _db.Countries.Where(x => x.CountryId == vm.SelectedCountry).First();
                star.Country = country;
                star.CountryId = country.CountryId;
                if (oldCountry.Stars.Count() != 0)
                {
                    oldCountry.Stars.Remove(star);
                }
                country.Stars.Add(star);
                _db.SaveChanges();

            }
        }

       

        public Star GetStar(int id)
        {
            var star = _db.Stars.Where(x => x.StarId == id).Select(s => new Star()
            {
                StarId= s.StarId,
                ProfilePictureURL= s.ProfilePictureURL,
                FirstName = s.FirstName,
                DateOfBirth= s.DateOfBirth,
                DateOfDeath= s.DateOfDeath,
                LastName = s.LastName,
                OtherName=s.OtherName,
                CountryId= s.CountryId,
                Country= s.Country
            }).First();
            return star;
        }
        public void Update(Star star)
        {
            _db.Update(star);
        }
    }
}
