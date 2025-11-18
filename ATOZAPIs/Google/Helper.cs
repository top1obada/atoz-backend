using ATOZDTO.ObjectsDTOs.ContactInformationDTO;
using ATOZDTO.ObjectsDTOs.CustomerDTO;
using ATOZDTO.ObjectsDTOs.PersonDTO;
using Google.Apis.Auth;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using System;
using System.Threading.Tasks;

public static class clsGoogleHelper
{
    public static async Task<GoogleJsonWebSignature.Payload?> ValidateIdTokenAsync(string idToken, string[] audiences)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = audiences
                });
            return payload;
        }
        catch
        {
            return null;
        }
    }

    public static async Task<(DateTime? Birthday, char? Gender)> GetExtraProfileDataAsync(string accessToken)
    {
        var initializer = new BaseClientService.Initializer
        {
            HttpClientInitializer = Google.Apis.Auth.OAuth2.GoogleCredential.FromAccessToken(accessToken),
            ApplicationName = "ATOZ-App"
        };

        var peopleService = new PeopleServiceService(initializer);

        var request = peopleService.People.Get("people/me");
        request.PersonFields = "birthdays,genders";

        Person profile = await request.ExecuteAsync();

        DateTime? birthday = null;
        if (profile.Birthdays != null && profile.Birthdays.Count > 0)
        {
            var b = profile.Birthdays[0].Date;
            if (b != null && b.Year.HasValue && b.Month.HasValue && b.Day.HasValue)
            {
                birthday = new DateTime(b.Year.Value, b.Month.Value, b.Day.Value);
            }
        }

        char? gender = null;
        if (profile.Genders != null && profile.Genders.Count > 0)
        {
            gender = profile.Genders[0].Value == "male" ? 'M'
                   : profile.Genders[0].Value == "female" ? 'F'
                   : 'N';
        }

        return (birthday, gender);
    }


    public static clsCustomerSignUpByEmailDTO GetCustomerSignUpByEmailInfo(GoogleJsonWebSignature.Payload PayLoad,
        (DateTime ?BirthDay,char ? Gender) ExtraInfo)
    {
        var SignUpByEmailDTO = new clsCustomerSignUpByEmailDTO()
        {
            Permissions = null,

            Person = new clsPersonDTO()
            {
                FirstName = PayLoad.Name,
                LastName = PayLoad.FamilyName,
                Gender = ExtraInfo.Gender,
                BirthDate = ExtraInfo.BirthDay,
                Nationality = "Syria",
                ContactInformation = new clsContactInformationDTO()
                {
                    Email = PayLoad.Email
                }

            }
        };

        return SignUpByEmailDTO;

    }
    
}