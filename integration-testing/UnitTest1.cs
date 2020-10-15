using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Alert_To_Care_Test.Repository;

namespace integration_testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllIcusWhenIcusArePresent()
        {
            ICUConfigTestRepository iCUConfigTestRepository = new ICUConfigTestRepository();
            var icu = iCUConfigTestRepository.GetAllICU();
            Assert.AreEqual(1, icu.Data[0].id);
            Assert.AreEqual(10, icu.Data[0].numberOfBeds);
            Assert.AreEqual('H', icu.Data[0].layout);
            Assert.AreEqual(HttpStatusCode.OK, icu.StatusCode);
        }


        [TestMethod]
        public void WhenIdIsPresentThenStatusIsOk()
        {
            ICUConfigTestRepository iCUConfigTestRepository = new ICUConfigTestRepository();
            var icu = iCUConfigTestRepository.GetICU(1);
            Assert.AreEqual(1, icu.Data.id);
            Assert.AreEqual(10, icu.Data.numberOfBeds);
            Assert.AreEqual('H', icu.Data.layout);
            Assert.AreEqual(HttpStatusCode.OK, icu.StatusCode);
        }

        [TestMethod]
        public void WhenIcuIdIsNotPresentThenStatusIsNotFound()
        {
            ICUConfigTestRepository iCUConfigTestRepository = new ICUConfigTestRepository();
            var icu = iCUConfigTestRepository.GetICU(172);
            Assert.AreEqual(HttpStatusCode.NotFound, icu.StatusCode);
        }

        [TestMethod]
        public void RegisterIcuWhenJsonFromBodyIsPosted()
        {
            ICUConfigTestRepository iCUConfigTestRepository = new ICUConfigTestRepository();
            var response = iCUConfigTestRepository.RegisterIcu();
            Assert.AreEqual(HttpStatusCode.OK, response);

        }
        [TestMethod]
        public void DeleteIcuWhenIdIsPresentThenStatusOk()
        {
            ICUConfigTestRepository iCUConfigTestRepository = new ICUConfigTestRepository();
            var response = iCUConfigTestRepository.DeleteIcu(72);
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [TestMethod]
        public void DeleteIcuWhenIdIsNotPresentThenStatusNotFound()
        {
            ICUConfigTestRepository iCUConfigTestRepository = new ICUConfigTestRepository();
            var response = iCUConfigTestRepository.DeleteIcu(125);
            Assert.AreEqual(HttpStatusCode.NotFound, response);
        }

        [TestMethod]
        public void WhenICUIsFullStatusNotFound()
        {
            OccupancyMgmtRepository occupancyMgmt = new OccupancyMgmtRepository();
            var response = occupancyMgmt.AddPatient(1);
            Assert.AreEqual(HttpStatusCode.NotFound, response);
        }

        [TestMethod]
        public void GetAllPatientWhenPatientsArePresent()
        {
            OccupancyMgmtRepository occupancyMgmt = new OccupancyMgmtRepository();
            var response = occupancyMgmt.GetAllPatient(1);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("ana", response.Data[0].name);
            Assert.AreEqual(6, response.Data[0].id);
        }

        [TestMethod]
        public void GetAllPatientWhenICUDoesntExist()
        {
            OccupancyMgmtRepository occupancyMgmt = new OccupancyMgmtRepository();
            var response = occupancyMgmt.GetAllPatient(139);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetPatientDetailsWhenPatientExists()
        {
            OccupancyMgmtRepository occupancyMgmt = new OccupancyMgmtRepository();
            var response = occupancyMgmt.GetPatientDetails(6);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("ana", response.Data.name);
            Assert.AreEqual("b+", response.Data.bloodGroup);

        }

        [TestMethod]
        public void GetPatientDetailsWhenPatientDoesNotExist()
        {
            OccupancyMgmtRepository occupancyMgmt = new OccupancyMgmtRepository();
            var response = occupancyMgmt.GetPatientDetails(26);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }


        [TestMethod]
        public void CheckIfPatientIsDeletedWhenExists()
        {
            OccupancyMgmtRepository occupancyMgmt = new OccupancyMgmtRepository();
            var response = occupancyMgmt.DeletePatient(23);
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [TestMethod]
        public void CheckIfPatientIsDeletedWhenDoesNotExist()
        {
            OccupancyMgmtRepository occupancyMgmt = new OccupancyMgmtRepository();
            var response = occupancyMgmt.DeletePatient(8);
            Assert.AreEqual(HttpStatusCode.NotFound, response);
        }

        [TestMethod]
        public void PostVitalsToCheckWhenInRange()
        {
            Alert_to_care.tests.Repository.VitalsCheckRepository vitalsCheck = new Alert_to_care.tests.Repository.VitalsCheckRepository();
            var response = vitalsCheck.CheckVitals(true);
            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public void PostVitalsToCheckWhenNotInRange()
        {
            Alert_to_care.tests.Repository.VitalsCheckRepository vitalsCheck = new Alert_to_care.tests.Repository.VitalsCheckRepository();
            var response = vitalsCheck.CheckVitals(false);
            Assert.AreEqual(true, response);
        }
    }
}
