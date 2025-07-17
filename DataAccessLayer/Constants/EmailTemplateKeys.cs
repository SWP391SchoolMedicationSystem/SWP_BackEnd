using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Constants
{
    public static class EmailTemplateKeys
    {
        public const string OtpEmail = "YÊU CẦU ĐẶT LẠI MẬT KHẨU";
        public const string PasswordChangeConfirmationEmail = "THÔNG BÁO BẢO MẬT";
        public const string VaccinationEventEmail = "THÔNG BÁO SỰ KIỆN TIÊM CHỦNG";
        public const string VaccinationResponseEmail = "FORM PHẢN HỒI TIÊM CHỦNG";
        public const string FormResponseEmail = "PHẢN HỒI ĐƠN";
        public const string FormResponseEmailWithFile = "PHẢN HỒI ĐƠN VỚI TẬP TIN";
        public const string FormResponseEmailWithFileAndLink = "PHẢN HỒI ĐƠN VỚI TẬP TIN VÀ LIÊN KẾT";
        public const string FormResponseEmailWithLink = "PHẢN HỒI ĐƠN VỚI LIÊN KẾT";
        public const string FormResponseEmailWithFileAndLinkAndQrCode = "PHẢN HỒI ĐƠN VỚI TẬP TIN, LIÊN KẾT VÀ MÃ QR";
    }
}
