using FormAPI.Models;

//namespace FormAPI.Service
//{
//    public class FormService
//    {
//        // Method to create a FormRecord object using FormField
//        public FormRecord CreateFormRecord(List<FormField> formFields)
//        {
//            var formRecord = new FormRecord();

//            foreach (var field in formFields)
//            {
//                switch (field.Name)
//                {
//                    case "FirstName":
//                        formRecord.FirstName = "";
//                        break;
//                    case "SecondName":
//                        formRecord.SecondName = ""; 
//                        break;
//                    case "ThirdName":
//                        formRecord.LastName = ""; 
//                        break;
//                    case "Birthdate":
                       
//                        break;
                   
//                    case "Gender":
//                        formRecord.Gender = ""; 
//                        break;
//                    case "LanguageCode":
//                        formRecord.LanguageCode = ""; 
//                        break;
//                    case "Nationality":
//                        formRecord.Nationality = ""; 
//                        break;
//                    case "PhoneNumber":
//                        formRecord.PhoneNumber = ""; 
//                        break;
//                    case "Email":
//                        formRecord.Email = ""; 
//                        break;
//                    case "Arrival":
//                        //let users set their own Arrival date
//                        break;
//                    case "Departure":
//                        // let users set their own Arrival date 
//                        break;
//                    case "Address":
//                        formRecord.Address = "";
//                        break;
//                    case "Zip":
//                        formRecord.Zip = ""; 
//                        break;
//                    case "City":
//                        formRecord.City = ""; 
//                        break;
//                    case "Country":
//                        formRecord.Country = ""; 
//                        break;
                   

//                }
//            }

//            return formRecord;
//        }
//    }


//}



//using FormAPI.Models;
//using System;
//using System.Collections.Generic;
//using Newtonsoft.Json;

namespace FormAPI.Service
{
    public class FormService
    {
        // Method to create a FormRecord object using FormField
        public FormRecord CreateFormRecord(List<FormField> formFields)
        {
            var formRecord = new FormRecord();

            foreach (var field in formFields)
            {
                switch (field.Kind)
                {
                    case "profile":
                        MapProfileField(formRecord, field);
                        break;
                    case "address":
                        MapAddressField(formRecord, field);
                        break;
                    case "registration":
                        MapRegistrationField(formRecord, field);
                        break;
                    default:
                        // Handle other kinds if needed
                        break;
                }
            }

            return formRecord;
        }

        private void MapProfileField(FormRecord formRecord, FormField field)
        {
            switch (field.FieldType)
            {
                case "profileId":
                    // Handle profileId if needed
                    break;
                case "firstName":
                    formRecord.FirstName = "";
                    break;
                case "middleName":
                    formRecord.SecondName = "";
                    break;
                case "lastName":
                    formRecord.LastName = "";
                    break;
                case "birthDate":
                    // Handle birthDate if needed
                    break;
                case "gender":
                    formRecord.Gender = "";
                    break;
                case "languageCode":
                    formRecord.LanguageCode = "";
                    break;
                case "nationality":
                    formRecord.Nationality = "";
                    break;
                case "phoneNumber":
                    throw new ArgumentException("Phone number is required.");
                    break;

                case "email":
                    formRecord.Email = "";
                    break;
                default:
                    // Handle other field types under profile if needed
                    break;
            }
        }

        private void MapAddressField(FormRecord formRecord, FormField field)
        {
            switch (field.FieldType)
            {
                case "addressId":
                    // Handle addressId if needed
                    break;
                case "addressLine":
                    formRecord.Address = "";
                    break;
                case "zip":
                    formRecord.Zip = "";
                    break;
                case "city":
                    formRecord.City = "";
                    break;
                case "country":
                    formRecord.Country = "";
                    break;
                default:
                    // Handle other field types under address if needed
                    break;
            }
        }

        private void MapRegistrationField(FormRecord formRecord, FormField field)
        {
            switch (field.FieldType)
            {
                case "arrival":
                    // Handle arrival if needed
                    break;
                case "departure":
                    // Handle departure if needed




























































































































































































































































































































































































































































































































































































































































































































                    break;
                default:
                    // Handle other field types under registration if needed
                    break;
            }
        }
    }
}

