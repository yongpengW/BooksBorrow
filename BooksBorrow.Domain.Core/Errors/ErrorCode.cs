using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.Errors
{
    public enum ErrorCode
    {
        #region basic 1xxxxxx

        Error = 0,
        SystemError = 1000001,

        #region database

        ObjectDoesNotFind = 1001001,
        CanNotDeleteUsedObject = 1001002,
        CanNotDeleteGroupHasUsers = 1001003,

        #endregion database

        #region command errors 1xx1xxx


        #endregion command errors

        CommandExecuteFail = 1001010,
        QueryExecuteFail = 1001020,

        #region permission errors 1xx2xxx

        No_Access_Permission = 1002000,
        No_Add_Permission = 1002001,
        No_Update_Permission = 1002002,
        No_View_Permission = 1002003,
        No_Delete_Permission = 1002004,
        No_Query_Permission = 1002005,

        No_Upload_Permission = 1002011,
        No_Download_Permission = 1002012,
        No_Import_Permission = 1002013,
        No_Expert_Permission = 1002014,

        #endregion permission errors

        #region form errors 1xx3xxx

        Field_Is_Required = 1003001,
        Field_Data_Format_Error = 1003002,

        #endregion form errors

        #endregion basic

        #region user 2xxxxxx

        #region user-account 2xx1xxx

        Signin_Username_Is_Required = 2001001,
        Signin_Password_Is_Required = 2001002,
        Signin_Email_Is_Required = 2001003,
        Signin_User_Dose_Not_Exists = 2001901,
        Signin_User_Is_Inactive = 2001902,
        Signin_User_Is_Deleted = 2001903,
        Signin_User_Is_No_Role = 2001904,
        Signin_User_Password_Wrong = 2001905,
        #endregion user 2xx1xxx



        #endregion user 2xxxxxx
        #region parameter 3xx1xxx
        Parameter_should_not_be_null = 3001000,
        pk_should_not_be_null = 3002000,
        Parameter_not_valid = 3003000
        #endregion
    }
}
