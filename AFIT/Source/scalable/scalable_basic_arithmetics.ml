(** Basic arithmetics for ordered euclidian ring. *)

open Scalable

(** Greater common (positive) divisor of two non-zero integers.
    @param bA non-zero bitarray.
    @param bB non-zero bitarray.
*)
let rec gcd_b bA bB = if bA=[] || bB= [] then invalid_arg "gcd:inputs must not be zero"
  else if (>>) bA bB then let bA= abs_b bA and bB = abs_b bB
                          in let c= mod_b bA bB
                             in if c=[] then bB else gcd_b bB c
  else let bB = abs_b bA and bA = abs_b bB
       in let c = mod_b bA bB
          in if c = [] then bB
            else gcd_b bB c;;

(** Extended euclidean division of two integers NOT OCAML DEFAULT.
    Given non-zero entries a b computes triple (u, v, d) such that
    a*u + b*v = d and d is gcd of a and b.
    @param bA non-zero bitarray.
    @param bB non-zero bitarray.
*)

let rec bezout_b bA bB = let r = mod_b bA bB
                         in let g = gcd_b bA bB
                            in let d = quot_b bA bB in
                               if r =[]
                               then ([],[0;1],g)
                               else  let (u,v,g) = bezout_b bB r in (v,diff_b u (mult_b v d),g);;
