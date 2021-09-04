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
        public bool AddCreditorOrDebtor(CreditorOrDebtor creditorOrDebtor)
        {
            try
            {
                if (_context.CreditorOrDebtor != null)
                {
                    _context.CreditorOrDebtor.Add(creditorOrDebtor);
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

        public bool EditCreditorOrDebtor(CreditorOrDebtor creditorOrDebtor)
        {
            try
            {
                if (_context.CreditorOrDebtor != null && _context.CreditorOrDebtor.Count > 0)
                {
                    if (creditorOrDebtor != null)
                    {
                        var credi = _context.CreditorOrDebtor.Where(x => x.id == creditorOrDebtor.id).FirstOrDefault();
                        if (credi != null)
                        {
                            _context.CreditorOrDebtor.Remove(credi);
                            _context.CreditorOrDebtor.Add(creditorOrDebtor);
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

        public bool DeleteCreditorOrDebtor(Guid creditorOrDebtorId)
        {
            try
            {
                if (_context.CreditorOrDebtor != null && _context.CreditorOrDebtor.Count > 0)
                {
                    var creditorOrDebtor = _context.CreditorOrDebtor.Find(x => x.id == creditorOrDebtorId);
                    _context.CreditorOrDebtor.Remove(creditorOrDebtor);
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

        public double UpdateCreditorBalance(Guid id, double price)
        {
            try
            {
                double resprice = 0;
                if (_context.CreditorOrDebtor != null && _context.CreditorOrDebtor.Count > 0)
                {
                    var UpdatedCreditor = _context.CreditorOrDebtor.FirstOrDefault(x => x.id == id);
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

        public List<CreditorOrDebtor> GetAllCreditorOrDebtor()
        {
            try
            {
                if (_context.CreditorOrDebtor != null)
                {
                    return _context.CreditorOrDebtor;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
            return null;
        }

        public List<CreditorOrDebtor> GetAllCreditorOrDebtor(String type)
        {
            try
            {
                if (_context.CreditorOrDebtor != null)
                {
                    if (type.Equals("CREDITOR"))
                    {
                        return _context.CreditorOrDebtor.Where(x => x.UserIdentity == Guid.Parse(Enumeration.Atributes[(int)Details.Creditor])).ToList();
                    }
                    else
                    {
                        return _context.CreditorOrDebtor.Where(x => x.UserIdentity == Guid.Parse(Enumeration.Atributes[(int)Details.Deptor])).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
            return null;
        }



        public bool AddInvoiceforCreditorOrDebtor(Invoice invoice)
        {
            try
            {
                if (_context.Invoices != null && invoice != null)
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
