using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT_DataLayer;
using SMT_DataLayer.Data;
namespace SMT_DataLayer.Repository
{
    public class SMT_DataRepository
    {
        SMT_DataContext _context;
        public SMT_DataRepository()
        {
            _context = new SMT_DataContext();
        }
        public bool AddCreditorOrDebitor(CreditorOrDebitor creditorOrDebitor)
        {
            try
            {
                if (_context.CreditorOrDebitor != null)
                {
                    _context.CreditorOrDebitor.Add(creditorOrDebitor);
                    _context.SaveChange();
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
       
        public bool EditCreditorOrDebitor(CreditorOrDebitor creditorOrDebitor)
        {
            try
            {
                if (_context.CreditorOrDebitor != null && _context.CreditorOrDebitor.Count > 0)
                {
                    if (creditorOrDebitor != null)
                    {
                        var credi = _context.CreditorOrDebitor.Where(x => x.id == creditorOrDebitor.id).FirstOrDefault();
                        if (credi != null)
                        {
                            _context.CreditorOrDebitor.Remove(credi);
                            _context.CreditorOrDebitor.Add(creditorOrDebitor);
                            _context.SaveChange((int)Details.Creditor);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
       
        public bool DeleteCreditorOrDebitor(CreditorOrDebitor creditorOrDebitor)
        {
            try
            {
                if (_context.CreditorOrDebitor != null && _context.CreditorOrDebitor.Count > 0)
                {
                    if (creditorOrDebitor != null)
                    {
                        _context.CreditorOrDebitor.Remove(creditorOrDebitor);
                        _context.SaveChange();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
      
        public double UpdateCreditorBalance(Guid id, double price)
        {
            try
            {
                double resprice = 0;
                if (_context.CreditorOrDebitor != null && _context.CreditorOrDebitor.Count > 0)
                {
                    var UpdatedCreditor = _context.CreditorOrDebitor.FirstOrDefault(x => x.id == id);
                    UpdatedCreditor.standingBalance -= price;
                    resprice = UpdatedCreditor.standingBalance;
                    _context.SaveChange((int)Details.Creditor);
                }
                return resprice;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return 0;
            }
        }
      
        public List<CreditorOrDebitor> GetAllCreditorOrDebitor()
        {
            try
            {
                if (_context.CreditorOrDebitor != null)
                {
                    return _context.CreditorOrDebitor;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
            return null;
        }
        
       
        public bool AddInvoiceforCreditorOrDebitor(Invoice invoice)
        {
            try
            {
                if (_context.Invoices != null&&invoice!=null)
                {
                   
                  _context.Invoices.Add(invoice);
                  _context.SaveChange();
                  return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
    }
}
