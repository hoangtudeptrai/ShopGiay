using static Azure.Core.HttpHeader;

namespace ShopGiay.StorePROC
{
    public class Store
    {
        #region DoiMatKhau
        //        ALTER PROC TuDH_DoiMatKhau --3, 'hoangtu', '12345678', '123456789'
        //	@ID_TaiKhoan INT = 0,
        //    @TaiKhoan NVARCHAR(500)= '',
        //	@MatKhauCu NVARCHAR(500) = '',
        //	@MatKhauMoi NVARCHAR(500) = ''
        //AS
        //BEGIN

        //    BEGIN TRAN

        //        BEGIN TRY

        //            DECLARE @ID_TaiKhoanSQL INT =  CASE
        //                                            WHEN EXISTS(SELECT 1 FROM dbo.TaiKhoan t WHERE t.TaiKhoan = @TaiKhoan AND ID_TaiKhoan = @ID_TaiKhoan AND MatKhau = @MatKhauCu)

        //                                            THEN(SELECT TOP 1 t.ID_TaiKhoan FROM dbo.TaiKhoan t WHERE t.TaiKhoan = @TaiKhoan  AND ID_TaiKhoan = @ID_TaiKhoan AND MatKhau = @MatKhauCu)

        //                                        ELSE -1 END;

        //			IF @ID_TaiKhoanSQL > 0

        //                BEGIN
        //                    UPDATE TaiKhoan
        //                    SET TaiKhoan = @TaiKhoan,
        //						MatKhau = @MatKhauMoi
        //                    WHERE ID_TaiKhoan = @ID_TaiKhoan

        //                    SELECT 1

        //                END
        //            ELSE

        //                BEGIN

        //                    SELECT -1 AS re

        //                END
        //        COMMIT TRAN
        //    END TRY
        //    BEGIN CATCH
        //        SELECT 0 AS re;
        //        ROLLBACK TRAN

        //    END CATCH
        //END
        #endregion

        #region XoaTaiKhoan
//        ALTER PROC TuDH_XoaTaiKhoan
//            @ID_TaiKhoan INT = 0
//AS
//BEGIN



//            BEGIN TRAN

//        BEGIN TRY

//        DECLARE @ID_TaiKhoanSQL INT =  CASE
//                                            WHEN EXISTS(SELECT 1 FROM dbo.TaiKhoan t WHERE ID_TaiKhoan = @ID_TaiKhoan)

//                                            THEN(SELECT TOP 1 t.ID_TaiKhoan FROM dbo.TaiKhoan t WHERE ID_TaiKhoan = @ID_TaiKhoan)

//                                                        ELSE -1 END;

//			IF @ID_TaiKhoanSQL > 0

//                BEGIN
//                DELETE TaiKhoan
//            WHERE ID_TaiKhoan = @ID_TaiKhoan
//            SELECT 1 AS re

//                END
//            ELSE

//                BEGIN

//                    SELECT -1 AS re

//                END
//        COMMIT TRAN
//    END TRYx
//    BEGIN CATCH

//        SELECT -1 AS re;
//        ROLLBACK TRAN

//    END CATCH
//END
        #endregion
    }
}
