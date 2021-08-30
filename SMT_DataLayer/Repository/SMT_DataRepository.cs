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
        public bool AddCreditor(Creditor creditor)
        {
            try
            {
               if(_context.Creditors != null)
                {
                    _context.Creditors.Add(creditor);
                    _context.SaveChange();
                }
                return true;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
        public bool AddDeptor(Deptor deptor)
        {
            try
            {
                if (_context.Deptors != null)
                {
                    _context.Deptors.Add(deptor);
                    _context.SaveChange();
                }
                return true;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
        public bool EditCreditor(Creditor creditor)
        {
            try
            {
                if (_context.Creditors != null && _context.Creditors.Count > 0)
                {
                    if (creditor != null)
                    {
                        var credi = _context.Creditors.Where(x => x.id == creditor.id).FirstOrDefault();
                        if (credi != null)
                        {
                            credi = creditor;
                            _context.SaveChange((int)Details.Creditor);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
        public bool EditDeptor(Deptor deptor)
        {
            try
            {
                if (_context.Deptors != null && _context.Deptors.Count > 0)
                {
                    if (deptor != null)
                    {
                        var dept = _context.Deptors.Where(x => x.id == deptor.id).FirstOrDefault();
                        if (dept != null)
                        {
                            dept = deptor;
                            _context.SaveChange((int)Details.Deptor);
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
        public bool DeleteCreditor(Creditor creditor)
        {
            try
            {
                if (_context.Creditors != null&& _context.Creditors.Count > 0)
                {
                    if (creditor != null)
                    {
                       return  _context.Creditors.Remove(creditor);
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
        public bool DeleteDeptor(Deptor deptor)
        {
            try
            {
                if (_context.Deptors != null && _context.Deptors.Count > 0)
                {
                    if (deptor != null)
                    {
                        return _context.Deptors.Remove(deptor);
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
            public double UpdateCreditorBalance(int id,double price)
        {
            try
            {
                double resprice = 0;
               if(_context.Creditors!=null && _context.Creditors.Count > 0)
                {
                    var UpdatedCreditor = _context.Creditors.FirstOrDefault(x => x.id == id);
                    UpdatedCreditor.standingBalance -= price;
                    resprice = UpdatedCreditor.standingBalance;
                    _context.SaveChange((int)Details.Creditor);
                }
                return resprice;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return 0;
            }
        }
        public double UpdateDeptorBalance(int id, double price)
        {
            try
            {
                double resPrice=0;
                if (_context.Deptors != null && _context.Deptors.Count > 0)
                {
                    var UpdatedDeptor = _context.Deptors.FirstOrDefault(x => x.id == id);
                    UpdatedDeptor.standingBalance -= price;
                    resPrice = UpdatedDeptor.standingBalance;
                    _context.SaveChange((int)Details.Deptor);
                }
                return resPrice;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return 0;
            }
        }
        public List<Creditor> GetAllCreditor()
        {
            try
            {
                if (_context.Creditors != null && _context.Creditors.Count > 0)
                {
                    return _context.Creditors;
                }
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
            }
            return null;
        }
        public List<Deptor> GetAllDeptor()
        {
            try
            {
                if (_context.Deptors != null && _context.Deptors.Count > 0)
                {
                    return _context.Deptors;
                }
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
            }
            return null;
        }
        public Invoice GetInvoiceforCreditor(int Id)
        {
            try
            {
                if (_context.Creditors != null && _context.Creditors.Count > 0)
                {
                    var creditorDetail = _context.Creditors.FirstOrDefault(x => x.id == Id);
                    Invoice invoice = new Invoice();
                    invoice.name = creditorDetail.name;
                    invoice.address = creditorDetail.address;
                    invoice.phone = creditorDetail.phone;
                    return invoice;
                }
                return null;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }
        public Invoice GetInvoiceforDeptor(int Id)
        {
            try
            {
                if (_context.Creditors != null && _context.Creditors.Count > 0)
                {
                    var DeptorDetail = _context.Deptors.FirstOrDefault(x => x.id == Id);
                    Invoice invoice = new Invoice();
                    invoice.name = DeptorDetail.name;
                    invoice.address = DeptorDetail.address;
                    invoice.phone = DeptorDetail.phone;
                    return invoice;
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }
        public bool AddInvoiceforCreditor(List<Invoice> invoicelist,int id)
        {
            try
            {
                if (invoicelist != null && invoicelist.Count > 0)
                {
                    int invoiceId = invoicelist[0].generateId();
                    foreach(Invoice invoice in invoicelist)
                    {
                        invoice.invoiceId = invoiceId;
                        invoice.forId = id;
                        invoice.forGuid = Guid.Parse(Enumeration.Atributes[(int)Details.Creditor]);
                        _context.Invoices.Add(invoice);
                    }
                    _context.SaveChange();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
        public bool AddInvoiceforDeptor(List<Invoice> invoicelist, int id)
        {
            try
            {
                if (invoicelist != null && invoicelist.Count > 0)
                {
                    int invoiceId = invoicelist[0].generateId();
                    foreach (Invoice invoice in invoicelist)
                    {
                        invoice.invoiceId = invoiceId;
                        invoice.forId = id;
                        invoice.forGuid = Guid.Parse(Enumeration.Atributes[(int)Details.Deptor]);
                        _context.Invoices.Add(invoice);
                    }
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
        public List<Invoice> SearchInvoice(string name)
        {
            try
            {
                if (_context.Invoices.Count > 0 && _context.Invoices != null)
                {
                    List<Invoice> result = new List<Invoice>();
                    foreach(Invoice invoice in _context.Invoices)
                    {
                        if (invoice.name.ToLower().Contains(name.ToLower()))
                        {
                            result.Add(invoice);
                        }
                    }
                    return result;
                }
                return null;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }
        public bool PayIn(int invoiceId,double amount)
        {
            try
            {
                var Invoice=_context.Invoices.Find(x => x.invoiceId == invoiceId);
                string name = "";
                double balance = 0;
                string payee = "";
                if (Invoice.forGuid.Equals(Guid.Parse(Enumeration.Atributes[(int)Details.Creditor])))
                {
                    balance=this.UpdateCreditorBalance(Invoice.forId, amount);
                    name = _context.Creditors.Where(x => x.id == Invoice.forId).Select(x => x.name).FirstOrDefault();
                    payee = "Creditor";
                }
                else if (Invoice.forGuid.Equals(Guid.Parse(Enumeration.Atributes[(int)Details.Deptor])))
                {
                    balance=this.UpdateDeptorBalance(Invoice.forId, amount);
                    name = _context.Deptors.Where(x => x.id == Invoice.forId).Select(x => x.name).FirstOrDefault();
                    payee = "Deptor";
                }
                PayIn_Out p = new PayIn_Out();
                p.name = name;
                p.standingBalance = balance;
                p.invoiceId = invoiceId;
                p.amount = amount;
                p.payer = payee;
                p.Date = DateTime.Now;
                _context.Payee.Add(p);
                return true;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
    }
}
