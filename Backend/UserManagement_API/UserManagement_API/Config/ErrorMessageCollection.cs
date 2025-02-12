namespace UserManagement_API.Config
{
    public class ErrorMessageCollection
    {
        /// <summary>
        /// Something Went Wrong, Got Null or Empty Values for One or More Required Properties. Please Recheck your Inputs and Try Again.
        /// </summary>
        internal static string MissingRequired => "Something Went Wrong, Got Null or Empty Values for One or More Required Properties. Please Recheck your Inputs and Try Again.";

        /// <summary>
        /// Unauthorized Action, You don't have Permission for the Following Operation.
        /// </summary>
        internal static string Unauthorized => "Unauthorized Action, You don't have Permission for the Following Operation.";

        /// <summary>
        /// Something Went Wrong, Update Operation Failed. Please Try Again.
        /// </summary>
        internal static string UpdateFailed => "Something Went Wrong, Update Operation Failed. Please Try Again.";

        /// <summary>
        /// Something Went Wrong, Delete Operation Failed. Please Try Again.
        /// </summary>
        internal static string DeleteFailed => "Something Went Wrong, Delete Operation Failed. Please Try Again.";

        /// <summary>
        /// Records Not Found, Didn't Get Any Results from the Database.
        /// </summary>
        internal static string RecordsNotFound => "Records Not Found, Didn't Get Any Results from the Database.";

        /// <summary>
        /// Something Went Wrong, Insert Operation Failed. Please Try Again.
        /// </summary>
        internal static string InsertFailed => "Something Went Wrong, Insert Operation Failed. Please Try Again.";

        /// <summary>
        /// Something Went Wrong, Got an Unexpected Error When Identifying the User. Please Try Again.
        /// </summary>
        internal static string IdentityError => "Something Went Wrong, Got an Unexpected Error When Identifying the User. Please Try Again.";

        /// <summary>
        /// Something Went Wrong, Cancel Operation Failed. Please Try Again.
        /// </summary>
        internal static string CancelFailed => "Something Went Wrong, Cancel Operation Failed. Please Try Again.";
    }
}
