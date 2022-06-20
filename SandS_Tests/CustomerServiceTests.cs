using Service.Repository.IRepository;
using NSubstitute;
using SandS.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Identity;
using Service.Repository;
using Service.Data;

namespace SandS_Tests
{
    public class CustomerServiceTests<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;
        //private readonly IRepository _sut;
        private readonly IUnityOfWork _unityofwork; 
        private readonly IUnityOfWork _iuofw = Substitute.For<IUnityOfWork>();

        private readonly IRepository<T> _repository = Substitute.For<IRepository<T>>();
        public CustomerServiceTests()
        {
            //_companyController =  new CompanyController(_unityofwork,)
        }

        [Test]
        public void GetAll_ShouldReturnCompanyList()
        {
            //Arrange
            const  int Id = 1;

            //Act
            var customer = _repository.GetAll();


            //Assert
        }
    }
}