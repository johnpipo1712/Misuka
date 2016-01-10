using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Misuka.Infrastructure.Smtp
{
  [Serializable]
  public class MailMessageWrapper
  {
    Boolean IsBodyHtml { get; set; }
    String Body { get; set; }
    MailAddressWrapper From { get; set; }
    IList<MailAddressWrapper> To = new List<MailAddressWrapper>();
    IList<MailAddressWrapper> CC = new List<MailAddressWrapper>();
    IList<MailAddressWrapper> Bcc = new List<MailAddressWrapper>();
    IList<MailAddressWrapper> ReplyTo = new List<MailAddressWrapper>();
    MailAddressWrapper Sender { get; set; }
    String Subject { get; set; }
    IList<AttachmentWrapper> Attachments = new List<AttachmentWrapper>();
    Encoding BodyEncoding;
    Encoding SubjectEncoding;
    DeliveryNotificationOptions DeliveryNotificationOptions;
    CollectionWrapper Headers;
    MailPriority Priority;
    IList<AlternateViewWrapper> AlternateViews = new List<AlternateViewWrapper>();

    /// 
    /// Creates a new serializeable mailmessage based on a MailMessage object
    /// 
    public MailMessageWrapper(MailMessage mm)
    {
      IsBodyHtml = mm.IsBodyHtml;
      Body = mm.Body;
      Subject = mm.Subject;
      From = MailAddressWrapper.GetSerializeableMailAddress(mm.From);
      foreach (MailAddress ma in mm.To)
      {
        To.Add(MailAddressWrapper.GetSerializeableMailAddress(ma));
      }
      foreach (MailAddress ma in mm.CC)
      {
        CC.Add(MailAddressWrapper.GetSerializeableMailAddress(ma));
      }
      foreach (MailAddress ma in mm.Bcc)
      {
        Bcc.Add(MailAddressWrapper.GetSerializeableMailAddress(ma));
      }
      foreach (Attachment att in mm.Attachments)
      {
        Attachments.Add(AttachmentWrapper.GetSerializeableAttachment(att));
      }

      BodyEncoding = mm.BodyEncoding;

      DeliveryNotificationOptions = mm.DeliveryNotificationOptions;
      Headers = CollectionWrapper.GetSerializeableCollection(mm.Headers);
      Priority = mm.Priority;

      foreach (MailAddress ma in mm.ReplyToList)
      {
        ReplyTo.Add(MailAddressWrapper.GetSerializeableMailAddress(ma));
      }

      Sender = MailAddressWrapper.GetSerializeableMailAddress(mm.Sender);
      SubjectEncoding = mm.SubjectEncoding;

      foreach (AlternateView av in mm.AlternateViews)
        AlternateViews.Add(AlternateViewWrapper.GetSerializeableAlternateView(av));
    }


    /// 
    /// Returns the MailMessge object from the serializeable object
    /// 
    public MailMessage GetMailMessage()
    {
      MailMessage mm = new MailMessage();

      mm.IsBodyHtml = IsBodyHtml;
      mm.Body = Body;
      mm.Subject = Subject;
      if (From != null)
        mm.From = From.GetMailAddress();

      foreach (MailAddressWrapper ma in To)
      {
        mm.To.Add(ma.GetMailAddress());
      }
      foreach (MailAddressWrapper ma in CC)
      {
        mm.CC.Add(ma.GetMailAddress());
      }
      foreach (MailAddressWrapper ma in Bcc)
      {
        mm.Bcc.Add(ma.GetMailAddress());
      }
      foreach (AttachmentWrapper att in Attachments)
      {
        mm.Attachments.Add(att.GetAttachment());
      }

      mm.BodyEncoding = BodyEncoding;
      mm.DeliveryNotificationOptions = DeliveryNotificationOptions;
      Headers.SetColletion(mm.Headers);
      mm.Priority = Priority;

      if (ReplyTo != null)
      {
        foreach (MailAddressWrapper ma in ReplyTo)
        {
          mm.ReplyToList.Add(ma.GetMailAddress());
        }
      }

      if (Sender != null)
        mm.Sender = Sender.GetMailAddress();

      mm.SubjectEncoding = SubjectEncoding;

      foreach (AlternateViewWrapper av in AlternateViews)
        mm.AlternateViews.Add(av.GetAlternateView());

      return mm;
    }

  }
}