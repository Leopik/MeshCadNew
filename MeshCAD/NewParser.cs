using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD
{
    class NewParser
    {
        struct ModelFirst {

        }

        public double sqr(double x) => Math.Pow(x, 2);

        public double[,] Parse(string filePath)
        {
            int NNSO = 2;
            // max kol soedin uzlov}
            int nplat = 20;
            // max kol plat}
            int KST = 30;
            // MAX KOL STOEK}
            int mpla = 20;
            // max kol plastin fiksir sh}
            int mkuz = 30;
            // max kol uzlov v stoike}

            double PI = 3.1462;
            int NQL = 1;
            int NW = 9;//диапазонов вибрации}
            int NKRE = 80;//МАКС узлов крепления}
            int NMA = 12;//макс сосред масс}
            int UZL = 400;//макс узлов}
            int NKON = 10;//макс контрольных узлов}
            int NUD = 6;//макс точек уд имп и после}
            int NNPR = 400;//МАКС колич прямоугольников}
            int NNTR = 8;//макс колич треуг}
            int NTPL = 200;//макс колич типов плоских эл}
            int NXST = 5;//макс колич типов стержней}
            int NNST = 50;//макс колич стержней}

            int NPLASF, NPLASR, NUZ, NP1, NNNA, NE, NPR, NTS, NST, NTR, NK, IP, IC, IL, IPPP, JP, J1, J2;
            int npv, nng, NAPR, TR, NBL, IZ, IJ, JL, JK, JN, JPPP, NK1, IFO;
            int NET, nsto, nste, nns, nnx, nkx, nny, nky, nnz, nkz, nx, ny, nz;
            int ntip, WWIB, WUD, JJ, NSM, ND, KMN, KF, L1, LL, MN, ERRO, NGW, NKT;
            int nprr, nv, ng, nlv, nvg, nob, NPU, ii1, NUR, N, NN, NTP, NKR, NDIP, NGI, NGJ, K;
            nob = 0;
            NE = 0;
            double DISP, PEX, PEY, PEZ, PER, me, AA, DF, F1, FK, FN, S1, S2, S3, W;
            double ss, sx;
            double sigma, kz, mob, sy, sz, dw, dt, LPER;
            int nupt, kolpr, NTMPL, nnn, ktip, IJ1, tnsg, nsg, m, p, g1, g2, g3, g4;
            double s, g;
            int nomel, nomuz, flag, tipr, nras, kolb, ntipr;
            ktip = 0;
            nsto = 0;
            kolpr = 0;
            ND = 0;
            StreamReader file = new StreamReader(filePath);
            file.ReadLine();
            NN = int.Parse(file.ReadLine());
            file.ReadLine();
            NPU = int.Parse(file.ReadLine());
            file.ReadLine();
            NPLASR = int.Parse(file.ReadLine());
            file.ReadLine();
            NPLASF = int.Parse(file.ReadLine());
            file.ReadLine();
            NST = int.Parse(file.ReadLine());
            file.ReadLine();
            NTR = int.Parse(file.ReadLine());
            file.ReadLine();
            NPR = int.Parse(file.ReadLine());
            file.ReadLine();
            NET = int.Parse(file.ReadLine());


            double[] cx = new double[30],

                cy = new double[30],
                    cz = new double[30],
                    x1 = new double[30],
                    x2 = new double[30],
                    x3 = new double[30],
                    y1 = new double[30],
                    y2 = new double[30],
                    y3 = new double[30],
                    z1 = new double[30],
                    z2 = new double[30],
                    z3 = new double[30];

            int[] nnps = new int[100],
                nps = new int[100],
                nnap = new int[100];
            int[,] nna = new int[nplat, 3];
            int[,] nost = new int[KST, 2];
            double[] mpst = new double[mpla];
            int[,] nlst = new int[mpla, mkuz];
            int[,] nust = new int[KST, mkuz];
            int[] kust = new int[mkuz];

            double[,] XARP = new double[NTPL, 5];
            double[,] COOR = new double[3, UZL];
            int[,] POLP = new int[NNPR, 5];
            int[,] POLT = new int[NNTR, 4];
            int[] LKR = new int[NKRE];
            int[] NUK = new int[NKON];
            int[] LSM = new int[NMA];
            double[] MSM = new double[NMA];
            int[,] POLS = new int[NNST, 4];
            double[,] XARS = new double[NXST, 11];
            int[,] LKRE = new int[NKRE, 2];
            if (NST > 0)
            {
                file.ReadLine();
                ND = int.Parse(file.ReadLine());
            }
            if ((NPU + NPLASR + NPLASF + NPR + NTR + NET) > 0)
            {
                file.ReadLine();
                NTMPL = int.Parse(file.ReadLine());
                ktip = NTMPL;
                for (int I = 0; I < NTMPL; I++)
                {

                    file.ReadLine();
                    for (int J = 0; J < 5; J++)
                        XARP[I, J] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                }
                /* I*/
            }
            /* if*/
            nprr = 0;
            if ((NPLASF + NET) > 0)
            {
                file.ReadLine();
                nx = int.Parse(file.ReadLine());
                ny = int.Parse(file.ReadLine());
                nz = int.Parse(file.ReadLine());
                file.ReadLine();
                for (int i = 0; i < nx; i++)
                {

                    file.ReadLine();
                    cx[i] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                }
                for (int i = 0; i < ny; i++)
                {

                    file.ReadLine();
                    cy[i] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                }
                for (int i = 0; i < nz; i++)
                {

                    file.ReadLine();
                    cz[i] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                }
                if (NET > 0)
                {
                    for (int i = 0; i < KST; i++)
                        kust[i] = 0;
                    file.ReadLine();
                    nsto = int.Parse(file.ReadLine());
                    file.ReadLine();
                    for (int i = 0; i < nsto; i++)
                    {
                        file.ReadLine();
                        nost[i, 0] = int.Parse(file.ReadLine());
                        nost[i, 1] = int.Parse(file.ReadLine());
                    }
                }
                /* if*/
                for (int npl = 0; npl < NPLASF; npl++) /* po plastinam*/
                {
                    file.ReadLine();
                    file.ReadLine();
                    nnps[npl] = int.Parse(file.ReadLine());
                    file.ReadLine();
                    nps[npl] = int.Parse(file.ReadLine());

                    if (NET > 0)
                    {
                        file.ReadLine();
                        mpst[npl] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                        file.ReadLine();
                        nlst[npl, 0] = int.Parse(file.ReadLine());
                        if (nlst[npl, 0] > 0)
                        {
                            file.ReadLine();
                            for (int i = 1; i < nlst[npl, 0] + 1; i++)
                                nlst[npl, i] = int.Parse(file.ReadLine());

                        }
                    }
                    file.ReadLine();
                    nnx = int.Parse(file.ReadLine());
                    nkx = int.Parse(file.ReadLine());
                    file.ReadLine();
                    nny = int.Parse(file.ReadLine());
                    nky = int.Parse(file.ReadLine());
                    file.ReadLine();
                    nnz = int.Parse(file.ReadLine());
                    nkz = int.Parse(file.ReadLine());
                    file.ReadLine();
                    ntip = int.Parse(file.ReadLine());

                    if (NET > 0)
                    {

                        for (int J = 0; J < 5; J++)
                            XARP[ktip, J] = XARP[ntip - 1, J];

                        XARP[ktip, 3] = mpst[npl] / XARP[ntip - 1, 0] / (cx[nkx - 1] - cx[nnx - 1]) / (cy[nky - 1] - cy[nny - 1]) * (Math.Pow(10, 9));
                        ktip = ktip + 1;
                    }
                    nprr = nps[npl] - 1;
                    NUZ = nnps[npl] - 1;
                    for (int i = nnz - 1; i < nkz; i++)
                        for (int j = nny - 1; j < nky; j++)
                            for (int k = nnx - 1; k < nkx; k++)
                            {
                                NE = NE + 1;/* kolitsh uzlov*/
                                NUZ = NUZ + 1;
                                COOR[0, NUZ - 1] = cx[k];
                                COOR[1, NUZ - 1] = cy[j];
                                COOR[2, NUZ - 1] = cz[i];
                                /*
                                    writeln('plastina=', npl, ' nuz=', nuz, ' ne=', ne,
                                   ' coor xyz=', COOR[1, nuz], ' ', COOR[2, nuz], ' ', COOR[3, nuz]);
                                */
                                if ((NET > 0) && (nsto > 0))
                                    if (i + 1 == nnz)
                                        for (int ij = 0; ij < nsto; ij++)
                                            if ((j + 1 == nost[ij, 1]) && (k + 1 == nost[ij, 0]))
                                                for (int L = 1; L < nlst[npl, 0] + 1; L++)
                                                    if (nlst[npl, L] == ij + 1)
                                                    {
                                                        nust[ij, kust[ij] + 1] = NUZ;
                                                        kust[ij] = kust[ij] + 1;
                                                    }/* L*/
                                                     /* formirovanie uzlov sterzhnei*/
                                                     /* IF j*/
                                                     /* if i*/;
                                /* if NET*/
                                /* uzli priamougolnikov nachaln levii nijnii*/
                                if ((Math.Abs(nny - nky) == 0) && (i + 1 != nkz) && (k + 1 != nkx))
                                {
                                    kolpr = kolpr + 1;

                                    POLP[nprr, 0] = NUZ + (nkx - nnx + 1);
                                    POLP[nprr, 1] = NUZ + (nkx - nnx + 2);
                                    POLP[nprr, 2] = NUZ;
                                    POLP[nprr, 3] = NUZ + 1;
                                    nprr = nprr + 1;
                                }
                                /* if*/
                                if ((Math.Abs(nnx - nkx) == 0) && (j + 1 != nky) && (i + 1 != nkz))
                                {
                                    kolpr = kolpr + 1;

                                    POLP[nprr, 0] = NUZ + (nky - nny + 1);
                                    POLP[nprr, 1] = NUZ + (nky - nny + 2);
                                    POLP[nprr, 2] = NUZ;
                                    POLP[nprr, 3] = NUZ + 1;
                                    nprr = nprr + 1;
                                }
                                /* if*/
                                if ((Math.Abs(nnz - nkz) == 0) && (j + 1 != nky) && (k + 1 != nkx))
                                {
                                    kolpr = kolpr + 1;

                                    POLP[nprr, 0] = NUZ + (nkx - nnx + 1);
                                    POLP[nprr, 1] = NUZ + (nkx - nnx + 2);
                                    POLP[nprr, 2] = NUZ;
                                    POLP[nprr, 3] = NUZ + 1;
                                    nprr = nprr + 1;
                                }
                                /* if*/
                                POLP[nprr - 1, 4] = ntip;
                                if (NET > 0)
                                    POLP[nprr - 1, 4] = ntip + npl + 1;
                                /*
                                    writeln('plastina=', npl, 'prymoug=', kolpr, ' nnn=', nnn, ' nuz=', nuz, ' nprr=', nprr, ' ne=', ne,
                                   '  POLP= ', POLP[nprr, 1], ' ', POLP[nprr, 2], ' ', POLP[nprr, 3], ' ',
                                   POLP[nprr, 4], ' ', POLP[nprr, 5]); read(ij);
                                */

                            }
                    /* k*/

                    if (npl + 1 == 1)
                        NE = NE - 11;
                    if (npl + 1 == 1)
                        NUZ = NUZ - 11;
                    /* !!!!!!!!!!!!!!!!!!!!!!!!!!!*/
                }
                /* npl*/
            }
            /* if nplasf > 0*/
            /*
                close(kff);
                goto per1;
            */
            if ((NPU + NPLASR) > 0)
            {
                nupt = 0;
                for (int npl = 0; npl < NPU + NPLASR; npl++) /* po platam i plast ravnom*/
                {
                    file.ReadLine();
                    file.ReadLine();
                    nna[npl, 0] = int.Parse(file.ReadLine());
                    file.ReadLine();
                    nnap[npl] = int.Parse(file.ReadLine());
                    file.ReadLine();

                    //WAS file.ReadLine(); x1[npl], y1[npl], z1[npl]);
                    file.ReadLine();
                    var coords = file
                        .ReadLine()
                        .Trim()
                        .Split(' ')
                        .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                        .ToArray();
                    x1[npl] = coords[0]; y1[npl] = coords[1]; z1[npl] = coords[2];

                    //WAS file.ReadLine(); x2[npl], y2[npl], z2[npl]);
                    file.ReadLine();
                    coords = file
                        .ReadLine()
                        .Trim()
                        .Split(' ')
                        .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                        .ToArray();
                    x2[npl] = coords[0]; y2[npl] = coords[1]; z2[npl] = coords[2];

                    //WAS file.ReadLine(); x3[npl], y3[npl], z3[npl]);
                    file.ReadLine();
                    coords = file
                        .ReadLine()
                        .Trim()
                        .Split(' ')
                        .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                        .ToArray();
                    x3[npl] = coords[0]; y3[npl] = coords[1]; z3[npl] = coords[2];
                    file.ReadLine();
                    ng = int.Parse(file.ReadLine());
                    nna[npl, 2] = ng;
                    file.ReadLine();
                    nv = int.Parse(file.ReadLine());
                    nna[npl, 1] = nv;
                    /* nnn = nv * ng + nnn; */
                    for (int j = 0; j < ng; j++)
                        for (int i = 0; i < nv; i++)
                        {

                            NUZ = nna[npl, 0] - 1 + nv * (j) + i + 1;/* po strokam numeracia uzlov, snizu vverh*/
                                                                     /* ne = ne + 1; */
                                                                     /* kolitsh uzlov v platah*/
                            if (NUZ > nupt)
                                nupt = NUZ;
                            /* max nomer uzla plati*/
                            COOR[0, NUZ - 1] = x1[npl] + (x2[npl] - x1[npl]) / (nv - 1) * (i) + (x3[npl] - x1[npl]) / (ng - 1) * (j);
                            COOR[1, NUZ - 1] = y1[npl] + (y3[npl] - y1[npl]) / (ng - 1) * (j) + (y2[npl] - y1[npl]) / (nv - 1) * (i);
                            COOR[2, NUZ - 1] = z1[npl] + (z3[npl] - z1[npl]) / (ng - 1) * (j) + (z2[npl] - z1[npl]) / (nv - 1) * (i);
                            if (Math.Abs(z1[npl] - z3[npl]) < 0.005)
                                COOR[2, NUZ] = z1[npl];
                            /* write(nuz, ' ', COOR[1, nuz], ' ', COOR[2, nuz], ' ', COOR[3, nuz]); */
                        }/* j*/;
                    /* i*/
                    file.ReadLine();
                    ntip = int.Parse(file.ReadLine());
                    nprr = nnap[npl] - 1;
                    for (int I = 0; I < ng - 1; I++)
                    {
                        for (int j = 0; j < nv - 1; j++)
                        {
                            kolpr = kolpr + 1;
                            
                            NUZ = nna[npl, 0] - 1 + nv * (I) + j + 1;
                            /* po strokam numeracia uzlov, snizu vverh*/
                            POLP[nprr, 0] = NUZ + nv;
                            /* uzli priamougolnikov nachaln levii nijnii*/
                            POLP[nprr, 1] = NUZ + nv + 1;
                            POLP[nprr, 2] = NUZ;
                            POLP[nprr, 3] = NUZ + 1;
                            POLP[nprr, 4] = ntip;
                            nprr = nprr + 1;
                            /* write(nprr, ' ', POLP[nprr, 1], ' ', POLP[nprr, 2], ' ', POLP[nprr, 3], ' ', POLP[nprr, 4], ' ', POLP[nprr, 5]); */
                        }
                        /* j*/
                    }
                    if (NPU > 0)
                    {
                        file.ReadLine();
                        nob = int.Parse(file.ReadLine());
                    }
                    if (nob > 0)
                        for (int L = 0; L < nob; L++)
                        {
                            file.ReadLine();
                            file.ReadLine();
                            nlv = int.Parse(file.ReadLine());
                            file.ReadLine();
                            npv = int.Parse(file.ReadLine());
                            file.ReadLine();
                            nng = int.Parse(file.ReadLine());
                            file.ReadLine();
                            nvg = int.Parse(file.ReadLine());
                            file.ReadLine();
                            mob = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                            file.ReadLine();
                            kz = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                            ktip = ktip + 1;
                            /* kol tipov elem*/
                            for (int i = nng - 1; i < nvg - 1; i++)/* po strokam snizu - vverh*/
                                for (int j = nlv - 1; j < npv - 1; j++)
                                {
                                    nprr = nnap[npl] - 1 + (nv - 1) * (i) + j + 1;/* nomera pramoug po strokam snizu - vverh,vverh - vpravo ot uzla*/
                                    POLP[nprr - 1, 4] = ktip/* +ntip*/;
                                }
                            XARP[ktip - 1, 0] = XARP[ntip - 1, 0];
                            XARP[ktip - 1, 1] = sqr(XARP[ntip - 1, 1]) * 10 * kz / (kz * XARP[ntip - 1, 1] + (1 - kz) * XARP[ntip - 1, 1] * 10);
                            XARP[ktip - 1, 2] = 0.32;
                            if (mob < 0.0001)
                            {
                                XARP[ktip - 1, 1] = 0.0001 * XARP[ntip - 1, 1];
                                XARP[ktip - 1, 2] = 0;
                            }
                            /* ushet virezov*/
                            sx = Math.Abs(COOR[0, POLP[nprr - 1, 0] - 1] - COOR[0, POLP[nprr - 1, 3] - 1]) / 1000.0;
                            sy = Math.Abs(COOR[1, POLP[nprr - 1, 0] - 1] - COOR[1, POLP[nprr - 1, 3] - 1]) / 1000.0;
                            sz = Math.Abs(COOR[2, POLP[nprr - 1, 0] - 1] - COOR[2, POLP[nprr - 1, 3] - 1]) / 1000.0;
                            if (sx < 0.0001)
                                sx = 1.0;
                            if (sy < 0.0001)
                                sy = 1.0;
                            if (sz < 0.0001)
                                sz = 1.0;
                            sx = sx * sy * sz * XARP[ntip - 1, 0] / 1000.0;
                            /*
                                ob'em 1-go priamoug*/
                            XARP[ktip - 1, 3] = XARP[ntip - 1, 3] + mob / ((npv - nlv) * (nvg - nng)) / sx;
                            if (mob < 0.0001)
                            {
                                XARP[ktip - 1/* +ntip*/, 1] = 0.1;
                                XARP[ktip - 1/* +ntip*/, 3] = 0;
                            }
                            /* ro*/
                            XARP[ktip - 1, 4] = XARP[ntip - 1, 4];
                        }/* po obobsh el*/;
                    /* if*/
                }
                /* po platam*/
                NE = nupt;
            }
            /* if NPU + nplasr > 0*/
            if (NN > 0)
            {
                file.ReadLine();
                for (int I = NE; I < NN + NE; I++)
                {

                    file.ReadLine();
                    for (int J = 0; J < 3; J++)
                        COOR[J, I] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                }
            }
            if (ND > 0)
            {
                file.ReadLine();
                for (int i = NN + NE; i < NN + ND + NE; i++)
                {

                    file.ReadLine();
                    for (int j = 0; j < 3; j++)
                        COOR[j, i] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                }
            }
            if (NPR > 0)
            {
                file.ReadLine();
                for (int I = kolpr; I < kolpr + NPR; I++)
                {

                    file.ReadLine();
                    for (int J = 0; J < 5; J++)
                        POLP[I, J] = int.Parse(file.ReadLine());

                }
            }
            if (NTR > 0)
            {
                file.ReadLine();
                for (int I = 0; I < NTR; I++)
                {

                    file.ReadLine();
                    for (int J = 0; J < 3; J++)
                        POLT[I, J] = int.Parse(file.ReadLine());

                    POLT[I, 3] = int.Parse(file.ReadLine());
                }
            }
            file.ReadLine();
            NKR = int.Parse(file.ReadLine());
            if (NKR > 0)
            {
                file.ReadLine();
                for (int i = 0; i < NKR; i++)
                    LKR[i] = int.Parse(file.ReadLine());

            }
            if (NET > 0)
            {
                file.ReadLine();
                for (int i = 0; i < nsto; i++)
                {
                    file.ReadLine();
                    int ij = int.Parse(file.ReadLine());
                    if (ij > 0)
                    {
                        LKRE[i, 0] = ij;
                        /* dobavlenie nomera uzla v stoike*/
                        nust[i, 0] = LKRE[i, 0];
                        kust[i] = kust[i] + 1;
                        NKR = NKR + 1;
                        /* obshee kolitsh uzlov kreplenia*/
                        LKR[NKR - 1] = LKRE[i, 0];
                    }
                    if (ij == 0)
                        for (int k = 0; k < kust[i] + 1; k++)
                            nust[i, k] = nust[i, k + 1];//CHECK CAREFULLY
                    file.ReadLine();
                    int j = int.Parse(file.ReadLine());
                    if (j > 0)
                    {
                        NKR = NKR + 1;
                        LKR[NKR - 1] = j;
                        kust[i] = kust[i] + 1;
                        nust[i, kust[i] - 1] = j;
                    }
                    /* writeln('uzli stoiki ', i, ' ij=', ij, ' j=', j, ' kust[i]=', kust[i], ' nust[i,kust[i]]=', nust[i, kust[i]]); */
                    if (nust[i, 0] == nust[i, 1])
                    {
                        kust[i] = kust[i] - 1;
                        for (int k = 0; k < kust[i]; k++)
                            nust[i, k] = nust[i, k + 1];

                    }
                    /*
                        WRITELN(fp, 'STOIKA ', I);
                        FOR K = 1 TO KUST[I]DO WRITE(fp,' ',nust[i, k]); WRITELN(fp);
                    */
                }
                /* nsto*/
            }
            /* if NET*/
            if (NST > 0)
            {
                file.ReadLine();
                for (int I = 0; I < NST; I++)
                {

                    file.ReadLine();
                    for (int J = 0; J < 3; J++)
                        POLS[I, J] = int.Parse(file.ReadLine());

                    POLS[I, 3] = int.Parse(file.ReadLine());
                }
            }
            MN = 0;
            for (int i = 0; i < nsto; i++)
                MN = MN + (kust[i] - 1);

            NST = NST + MN;
            /* kol stergnei*/
            if (NST > 0)
            {
                file.ReadLine();
                NTS = int.Parse(file.ReadLine());
                for (int I = 0; I < NTS; I++)
                {

                    file.ReadLine();
                    for (int J = 0; J < 11; J++)
                        XARS[I, J] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                }
            }
            if ((NST > 0) && (NET > 0))
            {
                file.ReadLine();
                file.ReadLine();
                for (int J = (NST - MN); J < NST; J++)
                {

                    file.ReadLine();
                    for (int k = 0; k < 4; k++)
                        POLS[J, k] = int.Parse(file.ReadLine());

                }
            }
            /* if*/
            file.ReadLine();
            NSM = int.Parse(file.ReadLine());
            if (NSM > 0)
            {
                file.ReadLine();
                for (int i = 0; i < NSM; i++)
                {

                    file.ReadLine();
                    LSM[i] = int.Parse(file.ReadLine());
                    file.ReadLine();
                    MSM[i] = double.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                }
            }
            file.ReadLine();
            NK1 = int.Parse(file.ReadLine());
            file.ReadLine();
            for (int i = 0; i < NK1; i++)
                NUK[i] = int.Parse(file.ReadLine());

            file.Close();
            NPR = NPR + kolpr;
            NN = NN + NE;

            int temp;
            if (NST > 0)
                for (int i = 0; i< NST; i++) 
                    {
                    temp = POLS[i, 1];
                        if (temp < POLS[i, 0])
                        {
                            POLS[i, 1] = POLS[i, 0];
                            POLS[i, 0] = temp;
                        }
                    }

            //SAVE
                    StreamWriter mshFile = new StreamWriter(@"E:\Dropbox\учебахуеба\диплом\progi\его прога\new_my.msh");
            var st3 = "$MeshFormat";
            mshFile.WriteLine(st3);
            mshFile.WriteLine("2.0" + " " + 0 + " " + 8);
            st3 = "$Nodes";
            mshFile.WriteLine(st3);
            nomuz = 0;
            mshFile.WriteLine(NN);
            for (int i = 0; i < NN; i++)
            {
                nomuz = nomuz + 1;
                mshFile.Write(nomuz.ToString() + " ");
                mshFile.Write(String.Format("{0:0.0}", COOR[0, nomuz - 1])
                    + " "
                    + String.Format("{0:0.0}", COOR[1, nomuz - 1] )
                    + " "
                    + String.Format("{0:0.0}", COOR[2, nomuz - 1] ));
                mshFile.WriteLine();

            }

            st3 = "$EndNodes";
            mshFile.WriteLine(st3);
            st3 = "$Elements";
            mshFile.WriteLine(st3);
            nomel = 0;
            mshFile.WriteLine(NST + NPR);
            if (NST > 0)
                for (int i = 0; i < NST; i++)
                {
                    nomel = nomel + 1;
                    mshFile.Write(nomel.ToString() + " " + 1 + " " + 0 + " ");
                    mshFile.Write(POLS[i, 0] + " " + POLS[i, 1]);
                    mshFile.WriteLine();
                }
            if (NPR > 0)
                for (int i = 0; i < NPR; i++)
                {
                    nomel = nomel + 1;
                    mshFile.Write(nomel.ToString()+ " 3 0 "+ POLP[i, 0]+ " "+ POLP[i, 1]+ " "+ POLP[i, 3]+ " "+ POLP[i, 2]);
                    mshFile.WriteLine();
                }

            st3 = "$EndElements";
            mshFile.WriteLine(st3);
            mshFile.Close();

            return COOR;
        }
    }
}
