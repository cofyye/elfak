#include<stdio.h>
#include<mpi.h>
#define N 5
int main(int argc, char** argv) {
	int rank, P; // P = 3
	int i, buffer[N] = { 1 , 2 , 3 , 4 , 5 }, sum = 0 , deljiv = 0;
	MPI_Status status;
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &P);
	if (rank == 0) {
		for (i = 0; i < N; i++)
		{
			sum += buffer[i];
		}
		for (i = 1; i < P; i++)
		{
			MPI_Send(&sum, 1, MPI_INT, i, 0, MPI_COMM_WORLD);
			MPI_Recv(&deljiv, 1, MPI_INT, i, 1, MPI_COMM_WORLD, &status);
			if (deljiv == 1)
				printf("rank :%d\n", i);
		}

	}
	else {
		MPI_Recv(&sum, 1, MPI_INT, 0, 0, MPI_COMM_WORLD ,  &status);
		if (sum % rank == 0)
			deljiv = 1;
		MPI_Send(&deljiv, 1, MPI_INT, 0, 1, MPI_COMM_WORLD);
	}
	MPI_Finalize();
	return 0;
}