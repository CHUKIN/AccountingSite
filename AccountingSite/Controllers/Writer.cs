﻿using AccountingSite.Models;
using System;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
//using Office = Microsoft.Office.Core;

namespace AccountingSite.Controllers
{
    public static class Writer
    {

        //public static void WriteCreate(Order order, string employee,string path)
        //{
        //    var app = new Word.Application();
        //    app.Visible = false;
        //    var doc = app.Documents.Add();
        //    var r = doc.Range();
        //    r.Text =order.Text+" "+employee;
        //    doc.SaveAs2($@"C:\1\{employee}{order.Id}.doc");

        //}
        public static void Write(Order order,string employee,string path)
        {
            Word.Application ap = new Word.Application();
            try
            {
                using (File.Create(path+$"{employee}{order.Id}.doc"))
                {

                }
                    Word.Document doc = ap.Documents.Open(path + $"{employee}{order.Id}.doc", ReadOnly: false, Visible: false);
                doc.Activate();

                Word.Selection sel = ap.Selection;

                if (sel != null)
                {
                    switch (sel.Type)
                    {
                        case Word.WdSelectionType.wdSelectionIP:
                            sel.TypeText("Дата: " + order.Date.ToString());
                            sel.TypeParagraph();

                            sel.TypeText("От кого: " + order.From.Name);
                            sel.TypeParagraph();

                            sel.TypeText("Кому: " + order.To.Name);
                            sel.TypeParagraph();

                            sel.TypeText("Номер документа: " + order.Id.ToString());
                            sel.TypeParagraph();

                            sel.TypeText("Текщее состояние документу: " + order.Status.Name);
                            sel.TypeParagraph();

                            sel.TypeText("Описание документа: " + order.Text);
                            sel.TypeParagraph();


                            sel.TypeText("Инвентарь: ");
                            sel.TypeParagraph();
                            foreach (var item in order.ItemTransactions)
                            {
                                sel.TypeText("Название: " + item.Name);
                                sel.TypeParagraph();
                                sel.TypeText("Количество: " + item.Count.ToString());
                                sel.TypeParagraph();
                            }
                            break;

                    }

                    // Remove all meta data.
                    doc.RemoveDocumentInformation(Word.WdRemoveDocInfoType.wdRDIAll);

                    ap.Documents.Save(NoPrompt: true, OriginalFormat: true);
                }
                else
                {
                   // Console.WriteLine("Unable to acquire Selection...no writing to document done..");
                }

                ap.Documents.Close(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
            }
            catch (Exception ex)
            {
               // Console.WriteLine("Exception Caught: " + ex.Message); // Could be that the document is already open (/) or Word is in Memory(?)
            }
            finally
            {
                ((Word._Application)ap).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ap);
            }
        }


    }
}