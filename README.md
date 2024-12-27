# İşçi idarəetmə sistemi   
Bu layihə işçilərin idarə edilməsini təmin etmək üçün hazırlanmış bir API-dır. İşçi məlumatlarını idarə etmək, yeni işçi əlavə etmək,mövcud işçilərin məlumatlarını yeniləmək və işçilərin məlumatlarını silmək üçün istifadə olunur.
## Başlanğıc
Bu API-ni işə salmaq üçün aşağıdakı addımları izləyin.
### Tələblər
- .NET Core SDK (məsələn: .NET 6.0)
- C# Proqramlaşdırma Dili
- Visual Studio
### Quraşdırma
1. Layihəni klonlayın:
    ```
    git clone https://gitlab.com/mirfaridagalarov/employee-management.git
    ```
 2. Layihə qovluğuna keçin:
    ```
    cd EmployeeManagement
    ```
### İşə Salma
API-ni aşağıdakı əmrlə işə sala bilərsiniz:
```
dotnet run
```


## API Sənədləşdirilməsi

### Company endpoint
##### 1.GET /api/company/getallcompany
##### 2.GET /api/company/getbyidcompany/{id}
##### 3.POST /api/company/addcompany
##### 4.PUT /api/company/editcompany
##### 5.PATCH /api/company/softdeletecompany/{id}

### Department endpoint
##### 1.GET /api/department/getalldepartment
##### 2.GET /api/department/getbyiddepartment/{id}
##### 3.POST /api/department/adddepartment
##### 4.PUT /api/department/editdepartment
##### 5.PATCH /api/department/softdeletedepartment/{id}

### Employee endpoint
##### 1.GET /api/employee/getallemployee
##### 2.GET /api/employee/getbyidemployee/{id}
##### 3.GET /api/employee/getallemployeesearch/{text}/{page}/{pageSize}
##### 4.POST /api/employee/addemployee
##### 5.PUT /api/employee/editemployee
##### 6.PATCH /api/employee/softdeleteemployee/{id}
