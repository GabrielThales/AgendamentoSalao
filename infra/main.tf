# 1. Definir o provedor
provider "aws" {
  region = "us-east-1"
}

# 2. Criar o Grupo de Segurança
resource "aws_security_group" "api_sg" {
  name        = "salao_api_sg"
  description = "Permitir trafego para a API do Salao"

  ingress {
    from_port   = 8080
    to_port     = 8080
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"] # Aberto para a internet (para o trabalho)
  }
 
  ingress {
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"] # Acesso SSH para o Ansible
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"] # Permitir saida total
  }
}

# 3. Criar a Máquina Virtual (EC2)
resource "aws_instance" "api_server" {
  ami           = "ami-0c7217cdde317cfec" # Ubuntu 22.04 LTS (us-east-1)
  instance_type = "t2.micro"             # Elegível para o nível gratuito
  key_name      = "ec2-aws-key-salao" # Adiciona esta linha (sem o .pem)
  
  vpc_security_group_ids = [aws_security_group.api_sg.id]

  tags = {
    Name = "Servidor-Salao-Beleza"
  }
}