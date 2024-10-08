I. Họ tên và mã số sinh viên các thành viên trong nhóm:
    + 21120093 - Trần Anh Kiệt
    + 21120105 - Trương Thành Nhân
    + 21120144 - Phạm Phúc Thuần


II. Các chức năng đã thực hiện:
1) Cơ sở (4.75 điểm): 
    + Màn hình đăng nhập (0.25 điểm)
    + Màn hình dashboard (0.25 điểm)
    + Quản lí hàng hóa (1.5 điểm)
    + Quản lí các đơn hàng (1.5 điểm)
    + Báo cáo thống kê (1 điểm)
    + Đóng gói thành file cài đặt (0.25 điểm) 
2) Gợi ý nâng cao (4.5 điểm):
    + Sử dụng một thiết kế giao diện tốt lấy từ pinterest (0.5 điểm)
    + Báo cáo các sản phẩm bán chạy trong tuần, trong tháng, trong năm (1 điểm)
    + Bổ sung khuyến mãi giảm giá (1 điểm)
    + Quản lí khách hàng (1 điểm)
    + Chương trình có khả năng mở rộng động theo kiến trúc plugin (1 điểm)


III. Các chức năng chưa thực hiện:
1) Cơ sở:
    + Cấu hình (0.25 điểm)
2) Gợi ý nâng cao:
    + Làm rối mã nguồn (obfuscator) chống dịch ngược (0.25 điểm)	
    + Thêm chế độ dùng thử - cho phép xài full phần mềm trong 15 ngày. Hết 15 ngày bắt đăng kí (mã code hay cách kích hoạt nào đó) (0.5 điểm)
    + Sử dụng giao diện Ribbon (0.25 điểm)
    + Backup / restore database (0.5 điểm)
    + Tổ chức theo mô hình 3 lớp (1 điểm)
    + Sử dụng mô hình MVVM (1 điểm)
    + Sử dụng Dependency injection (1 điểm)
    + Sử dụng DevExpress / Telerik  / Kendo UI  (1 điểm)
    + Có khả năng cập nhật tính năng mới qua mạng sử dụng ClickOnce(0.5 điểm)
    + Sử dụng thư viện WinUI mới (1 điểm)
    + Kết nối API Rest API (1 điểm)
    + Kết nối GraphQL API (1 điểm)
    + Tự động thay đổi sắp xếp hợp lí các thành phần theo độ rộng màn hình (responsive layout) (0.5 điểm)
  

IV. Các chức năng giáo viên nên xem xét cộng điểm vì đã bỏ nhiều thời gian và công sức tìm hiểu:
    + Sử dụng một thiết kế giao diện tốt lấy từ pinterest
    + Chương trình có khả năng mở rộng động theo kiến trúc plugin


V. Điểm tự đánh giá cho từng thành viên:
    + 21120093 - Trần Anh Kiệt - 10 điểm
    + 21120105 - Trương Thành Nhân - 10 điểm
    + 21120144 - Phạm Phúc Thuần - 10 điểm


VI. Link video: 
https://www.youtube.com/watch?v=YgaNxuHWEZs


VII. Một số lưu ý và hướng dẫn của nhóm:
1) Sau khi cài đặt từ file setup.exe: File mở chương trình là MyShopUI.exe, nên bật ở chế độ "Run as Administrator"
2) Cơ sở dữ liệu sử dụng: SQL Server 
    + Hướng dẫn và file sql nằm ở thư mục Database
    + File MyShop.sql chứa nội dung của cơ sở dữ liệu chính của chương trình, tên là MyShop 
    + Click chọn mở file thông qua phần mềm SQL Server Management Studio, Ctrl + A và nhấn F5 để khởi tạo cơ sở dữ liệu
3) Đăng nhập:
    + Settings: Cấu hình database để kết nối (tên server, tên database, tài khoản, mật khẩu)
    + Login: Phần đăng nhập sử dụng tài khoản được cấp (nằm trong database). Có 3 tài khoản sẵn có (Username: admin, Password: 1 / Username: sale1, Password: 1 / Username: sale2, Password: 1)
    + Màn hình sẽ bị lag 2-5 giây mới vào được màn hình chính (chưa khắc phục được)
4) File excel và hình ảnh:
    + Đều cố định, nằm trong folder Assets
5) Đơn hàng:
    + Mục Order Details List (khi xem thông tin một đơn hàng), cần Double Click để chỉnh sửa
    + Mục Products (khi xem thông tin một đơn hàng), nhấn Enter hoặc Tab để tìm kiếm
6) Báo cáo:
    + Mục Products (khi chọn xem thống kê số lượng sản phẩm), cần Double Click để xem thống kê sản phẩm
    + Mục Revenue And Profit, Click vào biểu đồ để xem thống kê các sản phẩm bán chạy
7) Kiến trúc plugin:
    + Áp dụng cho các file dll có tên bắt đầu bằng kí tự "_"