namespace Document.Library.Global.Enum
{
    public enum ValidationStatusCode
    {
        Valid = 0, //CanCreate = 0,
        DatasetLimitExceeded = 1,// CountLimit = 1,
        DatasetNameDuplicate = 2,//Duplicate = 2,
        GeocodeLimitExceeded = 3,// GeocodeLimit = 3,
        DatasetSizeLimitExceeded = 4,//  SizeLimit = 4,
        DatasetMissing = 5,
        DatasetNameMissing = 6,
        DatasetHeaderMissing = 7,
        DatasetAddressLatLngMissing = 8,
        DatasetInvalidId = 9,  //IdInValid
        DatasetRecordMissing = 10,
        DatasetPermissionDenied = 11,
        DatasetGeocoding = 12,


        MapLimitExceeded = 15,//    CountLimit = 1,
        MapMissing = 16,
        MapNameMissing = 17,
        MapInvalidId = 18, //IdInValid
        MapPermissionDenied = 19,

        RecordMissing = 30,
        RecordInvalidId = 31,
        RecordValueMissing = 32,


        ViewMissing = 40,
        ViewNameMissing = 41,
        ViewHeaderMissing = 42,
        ViewNameDuplicate = 43,
        ViewCriteriaMissing = 44,
        ViewInvalidId = 45,


        HeaderInvalidId = 50, //IdInValid

        StopPointsLimitExceeded = 70,//while creating each route, there is a limit on the number of stop-points

        //PermissionDenied = 19,
        // DatasetGeocoding = 20,// checking for replace data set.

        UserNotFound = 100,
        PasswordMismatch = 101,
        UserNotAssociatedToAccount = 102,
        UserEmailNotVerified = 103,
        NoLoginSlotAvailable = 104,


        EmailAlreadyExist = 105,
        BillingAdminLimitExceeded = 106,
        UserLimitExceeded = 107,
        CreateUserPermissionDenied = 108,

        InvalidApiKey = 150,

        AccountAdminCountExcceded = 300,
        AccountUserCountExcceded = 301,
        AccountBillingAdminCountExcceded = 302,

        InValid = 999

    }

}