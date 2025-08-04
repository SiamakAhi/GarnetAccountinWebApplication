using ClosedXML.Excel;
using GarnetAccounting.Areas.Commercial.Dtos;
using GarnetAccounting.Areas.Warehouse.Dto;
using GarnetAccounting.ViewModels.CommercialViewModel;

namespace GarnetAccounting.Classes
{
    public class ExcelImporter
    {
        public List<BuyerAddDto> ImportPersenFromExcel(ImportBuyerDto importDto, long sellerId, out List<string> errorMessages, int customerId = 0)
        {
            var buyerList = new List<BuyerAddDto>();
            errorMessages = new List<string>();
            var existingNames = new HashSet<string>();
            var existingNationalIds = new HashSet<string>();
            var existingEconomicCodes = new HashSet<string>();

            // Check if file is null or empty
            if (importDto.ExcelFile == null || importDto.ExcelFile.Length == 0)
            {
                errorMessages.Add("فایل اکسل معتبر نیست.");
                return buyerList;
            }

            using (var stream = new MemoryStream())
            {
                // Copy the file to memory stream
                importDto.ExcelFile.CopyTo(stream);
                stream.Position = 0; // Reset stream position for reading

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // Skipping header row

                    foreach (var row in rows)
                    {
                        // Mandatory fields
                        var name = row.Cell(3).GetValue<string>();
                        var legalStatusStr = row.Cell(2).GetValue<string>();

                        // Validation for mandatory fields
                        if (string.IsNullOrEmpty(name))
                        {
                            errorMessages.Add("نام شخص یا شرکت نباید خالی باشد.");
                            continue;
                        }
                        if (existingNames.Contains(name))
                        {
                            errorMessages.Add($"نام '{name}' در فایل تکراری است.");
                            continue;
                        }

                        // LegalStatus validation
                        if (string.IsNullOrEmpty(legalStatusStr) || !int.TryParse(legalStatusStr, out int legalStatus) || legalStatus < 1 || legalStatus > 4)
                        {
                            errorMessages.Add($"نوع شخص '{legalStatusStr}' برای '{name}' نامعتبر است. باید مقداری بین 1 تا 4 باشد.");
                            continue;
                        }

                        // Optional fields with duplicate checks based on AlowDuplicate
                        var nationalId = row.Cell(4).GetValue<string?>();
                        var economicCode = row.Cell(5).GetValue<string?>();

                        if (!importDto.AlowDuplicate)
                        {
                            if (importDto.CheckNationalId && !string.IsNullOrEmpty(nationalId) && existingNationalIds.Contains(nationalId))
                            {
                                errorMessages.Add($"شناسه ملی '{nationalId}' برای '{name}' در فایل تکراری است.");
                                continue;
                            }
                            if (!string.IsNullOrEmpty(economicCode) && existingEconomicCodes.Contains(economicCode))
                            {
                                errorMessages.Add($"کد اقتصادی '{economicCode}' برای '{name}' در فایل تکراری است.");
                                continue;
                            }
                        }

                        // Add Name, NationalId, and EconomicCode to hashsets for future duplicate checks
                        existingNames.Add(name);
                        if (!string.IsNullOrEmpty(nationalId)) existingNationalIds.Add(nationalId);
                        if (!string.IsNullOrEmpty(economicCode)) existingEconomicCodes.Add(economicCode);

                        var buyer = new BuyerAddDto
                        {
                            CustomerId = customerId,
                            AccountingSystemId = row.Cell(1).GetValue<string?>(),
                            SellerId = sellerId,
                            LegalStatus = Convert.ToInt16(legalStatus),
                            Name = name,
                            NationalId = nationalId,
                            EconomicCode = economicCode,
                            Province = row.Cell(6).GetValue<string?>(),
                            City = row.Cell(7).GetValue<string?>(),
                            PostalCode = row.Cell(8).GetValue<string?>(),
                            Address = row.Cell(9).GetValue<string?>(),
                            MobilePhone = row.Cell(10).GetValue<string?>(),
                            Fax = row.Cell(11).GetValue<string?>(),
                            InvoiceDescription = row.Cell(12).GetValue<string?>()
                        };

                        buyerList.Add(buyer);
                    }
                }
            }

            return buyerList;
        }
        public List<ProductBaseDto> ImportProductsFromExcel(IFormFile file, long sellerId, out List<string> errorMessages)
        {
            var productList = new List<ProductBaseDto>();
            errorMessages = new List<string>();

            // Check if file is null or empty
            if (file == null || file.Length == 0)
            {
                errorMessages.Add("فایل اکسل معتبر نیست.");
                return productList;
            }

            using (var stream = new MemoryStream())
            {
                // Copy the file to memory stream
                file.CopyTo(stream);
                stream.Position = 0; // Reset stream position for reading

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // Skipping header row

                    int rowIndex = 1; // To track row number

                    foreach (var row in rows)
                    {
                        rowIndex++; // Increment to match Excel row index (considering headers)

                        // Reading and mapping fields from Excel
                        var productCode = row.Cell(1).GetValue<string>();
                        var categoryId = row.Cell(2).GetValue<long?>();
                        var productName = row.Cell(3).GetValue<string>();
                        var baseUnitName = row.Cell(4).GetValue<string>();
                        var pakageUnitName = row.Cell(5).GetValue<string>();
                        var quantityInPakage = row.Cell(6).GetValue<int?>();
                        var uniqueId = row.Cell(7).GetValue<string?>();
                        var vatRate = row.Cell(8).GetValue<float?>();
                        var productType = row.Cell(9).GetValue<short?>();
                        var hasInventory = row.Cell(10).GetValue<bool?>();

                        // Validation for mandatory fields
                        if (string.IsNullOrEmpty(productName))
                        {
                            errorMessages.Add($"خطا در ردیف {rowIndex}: نام کالا یا خدمت نباید خالی باشد.");
                            continue;
                        }
                        if (string.IsNullOrEmpty(baseUnitName))
                        {
                            errorMessages.Add($"خطا در ردیف {rowIndex}: واحد شمارش نباید خالی باشد.");
                            continue;
                        }

                        // Set default values if necessary
                        if (vatRate == null)
                        {
                            vatRate = 0;
                        }
                        if (quantityInPakage == null && baseUnitName == pakageUnitName)
                        {
                            quantityInPakage = 1;
                        }

                        // Mapping values to DTO
                        var product = new ProductBaseDto
                        {
                            SellerId = sellerId,
                            ProductCode = productCode,
                            UniqueId = uniqueId,
                            ProductName = productName,
                            CategoryId = categoryId,
                            BaseUnitName = baseUnitName,
                            PakageUnitName = pakageUnitName,
                            QuantityInPakage = quantityInPakage ?? 1, // Default to 1 if null
                            VATRate = vatRate.Value,
                            ProductType = productType ?? 0, // Set a default product type if null
                            HasInventory = hasInventory ?? true,
                            IsService = (productType == 1),
                            IsActive = true // Set default active status
                        };

                        productList.Add(product);
                    }
                }
            }

            return productList;
        }
        public InvoiceImportDto_Atiran ReadInvoicesFromAtiranExcel(IFormFile file)
        {
            InvoiceImportDto_Atiran rawData = new InvoiceImportDto_Atiran();
            rawData.Items = new List<ImportRawData_Atiran>();
            rawData.Errors = new List<ErrorDto>();

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("فایل اکسل معتبر نیست.");
            }

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // Skipping header row
                    int rowNumber = 1;
                    foreach (var row in rows)
                    {
                        // Reading values into independent variables

                        string? invoiceNumber = row.Cell("A").GetValue<string?>();
                        try
                        { Convert.ToInt32(invoiceNumber); }
                        catch
                        { continue; }
                        string? strInvoiceDate = row.Cell("B").GetValue<string?>();
                        string? categoryName = null;
                        string? personCode = row.Cell("C").GetValue<string?>();
                        string personName = row.Cell("E").GetValue<string>();
                        string? personNationalId = row.Cell("AI").GetValue<string?>();
                        string? personEcconomicCode = row.Cell("AJ").GetValue<string?>();

                        string? productCode = row.Cell("G").GetValue<string?>() ?? "001";
                        string productName = row.Cell("H").GetValue<string>();
                        string? pakageUnitCountName = "بسته";
                        string? baseUnitCountName = "عدد";

                        int qtyBaseUnitInPakage = row.Cell("AH").GetValue<int?>() ?? 1;
                        decimal qtyPakage = row.Cell("I").GetValue<decimal?>() ?? 0;
                        decimal qtyBase = row.Cell("J").GetValue<decimal?>() ?? 0;
                        decimal fee = row.Cell("K").GetValue<decimal?>() ?? 0;
                        decimal priceBeforeDiscount = row.Cell("M").GetValue<decimal?>() ?? 0;
                        decimal discount = row.Cell("S").GetValue<decimal?>() ?? 0;
                        decimal priceAfterDiscount = priceBeforeDiscount - discount;

                        decimal vatRateInPercent = row.Cell("U").GetValue<decimal?>() ?? 0;
                        decimal vatPrice = Math.Round((priceAfterDiscount * vatRateInPercent) / 100);
                        decimal finalPrice = priceAfterDiscount + vatPrice;

                        DateTime? invoiceDate = null;
                        //validations
                        if (string.IsNullOrEmpty(invoiceNumber?.Trim()))
                        {
                            var error = new ErrorDto
                            {
                                Code = $"ردیف {rowNumber}",
                                Error = "شماره فاکتور قید نشده است."
                            };
                            rawData.Errors.Add(error);
                            rowNumber++;
                            continue;
                        }
                        try
                        {
                            invoiceDate = strInvoiceDate.PersianToLatin();
                        }
                        catch
                        {
                            var error = new ErrorDto
                            {
                                Code = $"ردیف {rowNumber}",
                                Error = "فرمت تاریخ نادرست است."
                            };
                            rawData.Errors.Add(error);
                            rowNumber++;
                            continue;
                        }
                        if (string.IsNullOrEmpty(personName))
                        {
                            var error = new ErrorDto
                            {
                                Code = $"ردیف {rowNumber}",
                                Error = "نام خریدار قید نشده است."
                            };
                            rawData.Errors.Add(error);
                            rowNumber++;
                            continue;
                        }
                        if (string.IsNullOrEmpty(productName))
                        {
                            var error = new ErrorDto
                            {
                                Code = $"ردیف {rowNumber}",
                                Error = "نام کالا یا خدمت قید نشده است."
                            };
                            rawData.Errors.Add(error);
                            rowNumber++;
                            continue;
                        }
                        if (qtyBase == 0 && qtyPakage == 0)
                        {
                            var error = new ErrorDto
                            {
                                Code = $"ردیف {rowNumber}",
                                Error = " مقدار کالا یا خدمت نادرست وارد شده است"
                            };
                            rawData.Errors.Add(error);
                            rowNumber++;
                            continue;
                        }
                        // Create InvoiceExcelImportDto object and add to list
                        var invoiceDto = new ImportRawData_Atiran
                        {
                            InvoiceNumer = invoiceNumber,
                            strDate = strInvoiceDate,
                            InvoiceDate = invoiceDate.Value,
                            CategoryName = categoryName,
                            personCode = personCode,
                            personName = personName,
                            personNationalId = personNationalId,
                            personEcconomicCode = personEcconomicCode,
                            ProductCode = productCode,
                            ProductName = productName,
                            PakageUnitCountName = pakageUnitCountName,
                            baseUnitCountName = baseUnitCountName,
                            QtyBaseUnitInPakage = qtyBaseUnitInPakage,
                            QtyPakage = qtyPakage,
                            QtyBase = qtyBase,
                            Fee = fee,
                            PriceBeforeDiscount = priceBeforeDiscount,
                            PriceAfterDiscount = priceAfterDiscount,
                            Discount = discount,
                            VatPrice = vatPrice,
                            VatRate = vatRateInPercent,
                            FinalPrice = finalPrice,

                        };

                        rawData.Items.Add(invoiceDto);
                        rowNumber++;
                    }
                }
            }

            return rawData;
        }


    }
}
