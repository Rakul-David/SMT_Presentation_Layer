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
                return false;
            }
        }
        public bool UpdateCreditorBalance(int id,double price)
        {
            try
            {
               if(_context.Creditors!=null && _context.Creditors.Count > 0)
                {
                    var UpdatedCreditor = _context.Creditors.FirstOrDefault(x => x.id == id);
                    UpdatedCreditor.standingBalance -= price;
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool UpdateDeptorBalance(int id, double price)
        {
            try
            {
                if (_context.Deptors != null && _context.Deptors.Count > 0)
                {
                    var UpdatedDeptor = _context.Deptors.FirstOrDefault(x => x.id == id);
                    UpdatedDeptor.standingBalance -= price;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Creditor> GetAllCreditor()
        {
            return _context.Creditors;
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
                    }
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool AddInvoiceforDepitor(List<Invoice> invoicelist, int id)
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
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
    }
}
