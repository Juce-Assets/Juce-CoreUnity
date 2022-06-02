using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Juce.Loc.Configuration;
using System.IO;
using static Google.Apis.Auth.OAuth2.ServiceAccountCredential;

namespace Juce.Loc.Dawers
{
	public static class TryGetSheetsServiceFromCredentials
	{
		public static bool Execute(
			LocalizationToolConfiguration localizationToolConfiguration,
			out SheetsService sheetsService
			)
		{
			string fullJsonPath = localizationToolConfiguration.CredentialsJsonPath;

			if(!File.Exists(fullJsonPath))
            {
				sheetsService = default;
				return false;
			}

			Stream jsonCredentialsStream = File.Open(fullJsonPath, FileMode.Open);

			ServiceAccountCredential credentials = FromServiceAccountData(jsonCredentialsStream);

			BaseClientService.Initializer initializer = new BaseClientService.Initializer()
			{
				HttpClientInitializer = credentials
			};

			sheetsService = new SheetsService(initializer);
			return true;
		}
	}
}
