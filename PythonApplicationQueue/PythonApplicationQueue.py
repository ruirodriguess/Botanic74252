from azure.storage.queue import QueueService
import smtplib
from email.mime.text import MIMEText

# Configurações do Azure Storage
storage_account_name = "saruirumos74252"
storage_account_key = "3B8VNSwQLx48BUS+I1TRZASRGRbRo/uYyB1pk2rZu87enJmtrAvAA4iCcdoXlVPjU3Z/R8z3KtVj+AStRq3k8g=="
queue_name = "queueruirumos"

# Configurações do email
smtp_server = "smtp.office365.com"
smtp_port = 587
smtp_username = "ruirodrigues04@outlook.pt"
smtp_password = ""
sender_email = "ruirodrigues04@outlook.pt"
receiver_email = "ruirodrigues04@outlook.pt"

def send_email(subject, body):
    msg = MIMEText(body)
    msg['Subject'] = subject
    msg['From'] = sender_email
    msg['To'] = receiver_email

    with smtplib.SMTP(smtp_server, smtp_port) as server:
        server.starttls()
        server.login(smtp_username, smtp_password)
        server.sendmail(sender_email, receiver_email, msg.as_string())

def process_message(queue_service, message):
    # Extrai as informações relevantes da mensagem
    # e envia o email com as informações
    subject = message.content
    body = message.content
    send_email(subject, body)

    # Exclui a mensagem da fila
    queue_service.delete_message(queue_name, message.id, message.pop_receipt)

def main():
    # Cria o serviço de fila do Azure Storage
    queue_service = QueueService(account_name=storage_account_name, account_key=storage_account_key)

    while True:
        # Recebe as mensagens da fila em um loop contínuo
        messages = queue_service.get_messages(queue_name, num_messages=1)

        if messages:
            for message in messages:
                process_message(queue_service, message)

if __name__ == "__main__":
    main()

