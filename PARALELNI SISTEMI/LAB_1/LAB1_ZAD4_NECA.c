#include<stdio.h>
#include<mpi.h>
#define N 6
int main(int argc, char** argv) {
	int rank, P; // P = 3
	int i, min, local_niz[N] , minimalni_niz[N] ,  niz[N] = {23 , 71 , 111 , 69 , 42 , 251};
	MPI_Status status;
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &P);
	if (N % P != 0) // error nisu deljivi
		return -1;
	if (rank == 0) { // P0
		for (i = 1; i <= P-1; i++) {
			MPI_Send(niz +(N/P)*(i), N / P, MPI_INT, i, 0, MPI_COMM_WORLD);
			MPI_Recv(minimalni_niz + i, 1, MPI_INT, i, 1, MPI_COMM_WORLD, &status);
		}
		min = niz[0];
		for (i = 0; i < N / P; i++) {
			if (min > niz[i])
				min = niz[i];
		}
		printf("rank : %d minimalni element:%d\n", rank, min);
		minimalni_niz[0] = min;
		min = minimalni_niz[0];
		for ( i = 1; i < P-1; i++)
		{
			if (min > minimalni_niz[i])
				min = minimalni_niz[i];

		}
		printf("minimalni element celog niza:%d\n",min);
		
	}
	else {
		MPI_Recv(local_niz, N / P, MPI_INT, 0, 0, MPI_COMM_WORLD, &status);
		min = local_niz[0];

		for ( i = 1; i < N/P; i++)
		{
			
			if (min > local_niz[i])
				min = local_niz[i];
		}
		printf("rank : %d salje minimalni element:%d\n", rank, min);
		MPI_Send(&min, 1, MPI_INT, 0, 1, MPI_COMM_WORLD);

	}
	MPI_Finalize();
	return 0;
}