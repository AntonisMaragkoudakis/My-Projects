#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <netinet/in.h>
#include <arpa/inet.h>

#define MAX 1024

int main ( int argc , char * argv []) {
	
	int portNumber, numChildren;
	char* programName;

	if( argc == 3 )          // An exw swsto arithmo orismatwn synexizw stin dimiourgia tou server alliws ektypwno lathos (sto telos tou kwdika)
	{
		programName = argv[0];
      	//printf("The programName is %s\n", programName);
      	portNumber = atoi(argv[1]);
      	//printf("The portNumber is %d\n", portNumber);
      	numChildren = atoi(argv[2]);
     	//printf("The numChildren is %d\n", numChildren);


    	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		int sockfd,recieve_Socket,ret,newSocket;             
		struct sockaddr_in serverAddr, back_addr,newAddr;
		socklen_t addr_size;


		sockfd = socket(AF_INET, SOCK_STREAM, 0);           // dimiourgw to socket tis TCP syndeshs pou tha akouei o pateras tous Clients
		if(sockfd < 0)
		{
			printf("[-]Error in connection.\n");
			exit(1);
		}
		printf("[+]Server Socket is created.\n");

		memset(&serverAddr, '\0', sizeof(serverAddr));      
		serverAddr.sin_family = AF_INET;                    
		serverAddr.sin_port = htons(portNumber);           // dinw ton arithmo tis thiras poy tha akoyei o server dld to portNumber
		serverAddr.sin_addr.s_addr = htonl(INADDR_ANY);

		ret = bind(sockfd, (struct sockaddr*)&serverAddr, sizeof(serverAddr));  // kanw bind
		if(ret < 0)
		{
			printf("[-]Error in binding.\n");
			exit(1);
		}
		printf("[+]Bind to port %d\n", portNumber);

		if(listen(sockfd, 10) == 0)             // kanw listen
		{
			printf("[+]Listening....\n");
		}
		else
		{
			printf("[-]Error in binding.\n");
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	 	pid_t parent_id;
		int i,p[2];
		char buffer[1024];
		char pipe_buffer[MAX];
		char temp_buffer[1024];
		char sending_back[1024];
		char* command;
		char* id_to_send = malloc(20);
		char* seira = malloc(20);
		FILE* fpopen;
		char ektelesmenh_entolh[1024];
		char* egkires_entoles[5]={"ls","cat","cut","grep","tr"};
	    char* command1;
		char* command2;


	    if (pipe(p) < 0)   // elegxw gia error tou pipe
		{
	        printf("Pipe Error");
	        exit(1);
		}

		for(int i=0; i<numChildren; i++)   // gennaw numChildren diergasies
		{
			parent_id = fork();
		}

		while(1) // leitourgeia server gia panta
		{

			if (parent_id !=0)     // an eisai pateras
			{
				newSocket = accept(sockfd, (struct sockaddr*)&newAddr, &addr_size);   // akou mesw TCP 
				if(newSocket < 0)
				{
					perror( "accept " );
					exit(1);
				}
				printf("Connection accepted from %s:%d\n", inet_ntoa(newAddr.sin_addr), ntohs(newAddr.sin_port));

				while(recv(newSocket, buffer, 1024, 0)>0)  //oso lamvaneis
				{
					close (p[0]);
					write(p[1], buffer, MAX);                          // dioxetefse tin pliroforia poy elaves mesw pipe sta paidia sou
					//printf("received from client= %s\n", buffer);
				}
			}
			else                      // alliws an eisai paidi
			{			
				close(sockfd);
				read(p[0], pipe_buffer, MAX);
				//printf("hrthe %s\n",pipe_buffer);
				strcpy(temp_buffer,pipe_buffer);
				id_to_send = strtok(pipe_buffer,".");     // spase tis plirofories pou exeis gia na vreis ta id_to_send , seira , command
				seira = strtok(NULL,"$");
				strtok(temp_buffer,"$");
				command =strtok(NULL,"$");
				//printf("meseira %s tha paei sthn %s me entolh %s\n",seira,id_to_send,command );


				// Creating socket file descriptor 
				if ( (recieve_Socket = socket(AF_INET, SOCK_DGRAM, 0)) < 0 )  //  dimiourgise to UDB socket apou tha apantiseis 
				{ 
					perror("UDP socket creation failed"); 
					exit(EXIT_FAILURE); 
				} 
				// Filling server information
				memset(&back_addr, 0, sizeof(back_addr));  
				back_addr.sin_family = AF_INET; 
				back_addr.sin_port = htons(atoi(id_to_send));  // tha steileis sto id_to_send
				back_addr.sin_addr.s_addr = INADDR_ANY; 
			    //////////////////////////////////////
						

				strcpy(sending_back, id_to_send);  
				strcat(sending_back, ".");
				strcat(sending_back, seira);      // kataskevh tis pliroforias pou tha steileis
				strcat(sending_back, "$");

				command1=strtok(command,";");
				command2=strtok(command1,"|");  // to command2 periexei thn leksi tis entolh
				for (int k=0; k<5; k++)
				{
					if(strncmp(command2, egkires_entoles[k], strlen(egkires_entoles[k])) == 0)  // an h entolh einai egkyrh
					{
						strcat(command," 2>&1"); // sthn periptwsh pou h entolh den ektelestei swsta na mporei na synexizei to terminal thn leitourgia tou
						fpopen = popen(command,"r");   
						if (fpopen == NULL) 
						{
					    	printf("Failed to run command\n" );  // An h entolh den mporei na ektelethei ektypwse error
					    	exit(1);
					  	}
						while (fgets(ektelesmenh_entolh, sizeof(ektelesmenh_entolh), fpopen) != NULL) // oso ekteleite h entolh
						{
					    	strcat(sending_back, ektelesmenh_entolh);  // kane thn concatenate se ayto pou prepei na steileis ston Client
					  	}
					} // an den einai egyrh h entolh teleftaio symvolo tou sending_back tha einai to $ kai etsi o Client den tha dimiourgisei keno arxeio
				}


                // edw stelnw to sending_back ston Client mesw tou back_addr sto opoio exei ginei idi to katalilo memset
				sendto(recieve_Socket, (const char *)sending_back, strlen(sending_back), MSG_CONFIRM, (const struct sockaddr *) &back_addr, sizeof(back_addr));  
				//printf("sending to client= %s\n", sending_back);
				bzero(buffer, sizeof(buffer));	// midenismos tou buffer
			}
		}

		close(newSocket); //kleinw to socket tis TCP otan kleisei h while(1) dld stamatisei h leitourgeia tou server
    }
    else if( argc > 3 ) 
    {
      printf("Too many arguments supplied.\n");
    }
    else                                                // Ektyposh lathous arithmou orismatwn. Tipota apo ta parapanw den tha exei simvei an 
    {													// exei symvei ayto 
      printf("More arguments are expected.\n");
    }

    return 0;
}

