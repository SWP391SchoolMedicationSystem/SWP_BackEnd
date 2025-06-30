namespace DataAccessLayer.DTO
{
    public class VaccinationEmailTemplate
    {
        public static EmailDTO GetDefaultVaccinationEmailTemplate()
        {
            return new EmailDTO
            {
                Subject = "Thông báo về sự kiện tiêm chủng: {EventName}",
                Body = @"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                            <h2 style='color: #2c3e50; text-align: center;'>Thông báo về sự kiện tiêm chủng</h2>
                            
                            <div style='background-color: #f8f9fa; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                                <h3 style='color: #e74c3c; margin-top: 0;'>Sự kiện: {EventName}</h3>
                                
                                <p><strong>Ngày diễn ra:</strong> {EventDate}</p>
                                <p><strong>Địa điểm:</strong> {Location}</p>
                                <p><strong>Mô tả:</strong> {Description}</p>
                                
                                {CustomMessage}
                            </div>
                            
                            <div style='background-color: #e8f5e8; padding: 15px; border-radius: 8px; margin: 20px 0;'>
                                <h4 style='color: #27ae60; margin-top: 0;'>Hướng dẫn phản hồi:</h4>
                                <p>Vui lòng click vào link bên dưới để xác nhận việc tham gia tiêm chủng cho con của bạn:</p>
                                <p style='text-align: center;'>
                                    <a href='{ResponseLink}' 
                                       style='background-color: #3498db; color: white; padding: 12px 24px; 
                                              text-decoration: none; border-radius: 5px; display: inline-block;'>
                                        Phản hồi ngay
                                    </a>
                                </p>
                            </div>
                            
                            <div style='background-color: #fff3cd; padding: 15px; border-radius: 8px; margin: 20px 0;'>
                                <h4 style='color: #856404; margin-top: 0;'>Lưu ý quan trọng:</h4>
                                <ul>
                                    <li>Vui lòng phản hồi trong vòng 7 ngày kể từ ngày nhận email này</li>
                                    <li>Nếu không thể tham gia, vui lòng nêu rõ lý do</li>
                                    <li>Thông tin này sẽ được sử dụng để lập kế hoạch tiêm chủng</li>
                                    <li>Mọi thắc mắc vui lòng liên hệ với nhà trường</li>
                                </ul>
                            </div>
                            
                            <div style='text-align: center; margin-top: 30px; padding-top: 20px; border-top: 1px solid #ddd;'>
                                <p style='color: #7f8c8d; font-size: 14px;'>
                                    Email này được gửi tự động từ hệ thống quản lý y tế học đường.<br>
                                    Vui lòng không trả lời email này.
                                </p>
                            </div>
                        </div>
                    </body>
                    </html>"
            };
        }

        public static EmailDTO GetReminderEmailTemplate()
        {
            return new EmailDTO
            {
                Subject = "Nhắc nhở: Chưa nhận được phản hồi về sự kiện tiêm chủng {EventName}",
                Body = @"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                            <h2 style='color: #e74c3c; text-align: center;'>Nhắc nhở quan trọng</h2>
                            
                            <div style='background-color: #fdf2e9; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                                <h3 style='color: #d35400; margin-top: 0;'>Sự kiện: {EventName}</h3>
                                
                                <p>Chúng tôi chưa nhận được phản hồi của bạn về sự kiện tiêm chủng sắp diễn ra.</p>
                                
                                <p><strong>Ngày diễn ra:</strong> {EventDate}</p>
                                <p><strong>Địa điểm:</strong> {Location}</p>
                                
                                <p style='color: #e74c3c; font-weight: bold;'>
                                    Vui lòng phản hồi ngay để chúng tôi có thể lập kế hoạch phù hợp.
                                </p>
                            </div>
                            
                            <div style='background-color: #e8f5e8; padding: 15px; border-radius: 8px; margin: 20px 0;'>
                                <h4 style='color: #27ae60; margin-top: 0;'>Phản hồi ngay:</h4>
                                <p style='text-align: center;'>
                                    <a href='{ResponseLink}' 
                                       style='background-color: #e74c3c; color: white; padding: 12px 24px; 
                                              text-decoration: none; border-radius: 5px; display: inline-block;'>
                                        Phản hồi ngay
                                    </a>
                                </p>
                            </div>
                            
                            <div style='text-align: center; margin-top: 30px; padding-top: 20px; border-top: 1px solid #ddd;'>
                                <p style='color: #7f8c8d; font-size: 14px;'>
                                    Email này được gửi tự động từ hệ thống quản lý y tế học đường.<br>
                                    Vui lòng không trả lời email này.
                                </p>
                            </div>
                        </div>
                    </body>
                    </html>"
            };
        }
    }
} 